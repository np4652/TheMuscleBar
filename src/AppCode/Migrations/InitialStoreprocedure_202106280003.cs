using FluentMigrator;

namespace TheMuscleBar.AppCode.Migrations
{
    [Migration(202106280003)]
    public class InitialStoreprocedure_202106280003 : Migration
    {
        public override void Down()
        {
            //Delete.Table("Employees");
            //Delete.Table("Companies");
        }

        public override void Up()
        {
			//Execute.EmbeddedScript("Database.Migrations.201607150000_Initial_StoreProcedure.sql");
            Execute.Sql(@"Create proc [dbo].[AddUser](@Id int,@SecurityStamp nvarchar(max)='' ,@PhoneNumberConfirmed bit,              
                                                     @PhoneNumber nvarchar(15)='',@PasswordHash nvarchar(max) ,@NormalizedUserName nvarchar(256) ,               
                                                     @NormalizedEmail nvarchar (256) ,@LockoutEnd datetimeoffset(7) = '',@LockoutEnabled bit,
                                                     @EmailConfirmed bit ,@Email nvarchar(256) ,@ConcurrencyStamp nvarchar(max) ,@AccessFailedCount int,
                                                     @TwoFactorEnabled bit,@UserName nvarchar(256),@Role varchar(30),@Name varchar(100),
                                                     @Gender char(1),@DOB varchar(11),@Address varchar(160),@AdharNo varchar(20),@MaritalStatus char(1),
                                                     @Occupation varchar(15),@ReferBy varchar(10),@MembershipType char(1)
                                                     )                
                            AS            
                            BEGIN            
                             Begin Try            
                              Begin Tran      
                               insert into Users (UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,
                                                  PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount,[Name],IsActive,
                                                  Gender,DOB,Address,AdharNo,MaritalStatus,Occupation,ReferBy,MembershipType,EntryOn)
                                           values(@UserName,@NormalizedUserName,@Email,@NormalizedEmail,@EmailConfirmed,@PasswordHash,ISNULL(@SecurityStamp,''),
                                                  @ConcurrencyStamp,ISNULL(@PhoneNumber,''),@PhoneNumberConfirmed,@TwoFactorEnabled,ISNULL(@LockoutEnd,GetDate()),
                                                  @LockoutEnabled,@AccessFailedCount,@Name,1,@Gender,@DOB,@Address,@AdharNo,@MaritalStatus,@Occupation,0,@MembershipType,Getdate())
                               Select @Id = SCOPE_IDENTITY()            
                               Declare @RoleId int            
                               Select @RoleId = Id from ApplicationRole(nolock) where [Name]=@Role            
                               insert into UserRoles(UserId,RoleId) values(@Id,@RoleId)            
                              Commit Tran            
                             End Try            
                             Begin Catch            
                              Rollback Tran            
                              SET NOCOUNT ON    
                              DECLARE @ErrorNumber varchar(15) = Error_Number(),@ERRORMESSAGE varchar(max)=ERROR_MESSAGE();    
                              insert into ErrorLog(ErrorMsg,ErrorNumber,ErrorFrom,EntryOn) values(@ERRORMESSAGE,@ErrorNumber,'AddUser',GETDATE());    
                              --declare @t table (n int not null unique);    
                              THROW 50000,@ERRORMESSAGE,1;    
                             End Catch            
                             end");
            Execute.Sql(@"CREATE Proc [dbo].[UpdateUser]
							                  @Id int,
							                  @PasswordHash nvarchar(max) ,							
							                  @TwoFactorEnabled bit,
							                  @RefreshToken varchar(256) = '',
							                  @RefreshTokenExpiryTime varchar(256) = null
                            AS    
                            BEGIN    
                             IF ISNULL(@PasswordHash,'')<>''  
                            	Update Users Set PasswordHash=@PasswordHash where Id=@Id
                             IF ISNULL(@RefreshToken,'')<>''
                            	Update Users Set RefreshToken=@RefreshToken , RefreshTokenExpiryTime = @RefreshTokenExpiryTime where Id=@Id
                            
                            END");
            Execute.Sql(@"CREATE proc proc_SaveNLog @msg varchar(max),@level varchar(max),@exception varchar(max),
                                                    @trace varchar(max),@logger varchar(max)  
                          As  
                          Begin
                          	INSERT INTO [NLogs]([When],[Message],[Level],Exception,Trace,Logger) VALUES (getutcdate(),@msg,@level,@exception,@trace,@logger)  
                          End  ");
            Execute.Sql(@"CREATE proc proc_getUserRole      
@Id bigint=0,      
@Email varchar(120)='',  
@mobileNo varchar(12)=''  
as      
begin      
if(@Id=0 and @Email='')      
begin      
select * from UserRoles  where 1=2      
return       
end      
if(@Id=0 and @Email<>'')      
begin      
select  @Id=Id from Users where NormalizedUserName=@Email      
end      
      
select RoleId from UserRoles where UserID=@Id      
end");
            Execute.Sql(@"Create proc proc_CollectFee
                            							@UserId int,
                            							@Amount numeric(18,2),
                            							@Discount numeric(18,2),
                            							@FromDate varchar(10),
                            							@ToDate varchar(10),
                            							@TransactionType tinyint,
                            							@PaymentMode  tinyint,
                            							@EntryBy int
                            AS
                            BEGIN
                            	BEGIN TRY
                            		BEGIN TRAN
                            			DECLARE @LedgerId int
                            			insert into Ledger(UserId,TransactionType,Amount,discount,PaymentMode,EntryOn,EntryBy) Values (@UserId,@TransactionType,@Amount,@discount,@PaymentMode,GETDATE(),@EntryBy)
                            			SET @LedgerId = SCOPE_IDENTITY()
                            			Insert into UserSubscription(UserId,DateFrom,DateTo,LedgerId) values (@UserId,@FromDate,@ToDate,@LedgerId)
                            			Select 1 StatusCode,'Success' ResponseText
                            		COMMIT
                            	END TRY
                            	BEGIN CATCH
                            		ROLLBACK
                            		Select -1 StatusCode,'Something went wrong' ResponseText
                            	END CATCH
                            END");
        }
    }
}

﻿using FluentMigrator;

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
            Execute.Sql(@"Create proc [dbo].[AddUser](@Id int,@UserId varchar(60),@SecurityStamp nvarchar(max)='' ,@PhoneNumberConfirmed bit,              
                                                     @PhoneNumber nvarchar(15)='',@PasswordHash nvarchar(max) ,@NormalizedUserName nvarchar(256) ,               
                                                     @NormalizedEmail nvarchar (256) ,@LockoutEnd datetimeoffset(7) = '',@LockoutEnabled bit,
                                                     @EmailConfirmed bit ,@Email nvarchar(256) ,@ConcurrencyStamp nvarchar(max) ,@AccessFailedCount int   ,            
                                                     @TwoFactorEnabled bit,@UserName nvarchar(256),@Role varchar(30),@Name varchar(100),@FOSID int        
                                                     )                
                            AS            
                            BEGIN            
                             Begin Try            
                              Begin Tran      
                               insert into Users (UserId, UserName,     NormalizedUserName,     Email,     NormalizedEmail,     EmailConfirmed,     PasswordHash,                 SecurityStamp,              ConcurrencyStamp,                
                                    PhoneNumber,     PhoneNumberConfirmed,     TwoFactorEnabled,     LockoutEnd,     LockoutEnabled,     AccessFailedCount,[Name],IsActive)                   
                                  values(@UserId, @UserName,@NormalizedUserName,     @Email,     @NormalizedEmail,     @EmailConfirmed,     @PasswordHash,  ISNULL                (@SecurityStamp,''),         @ConcurrencyStamp,                
                                     ISNULL(@PhoneNumber,''),     @PhoneNumberConfirmed,     @TwoFactorEnabled,ISNULL(@LockoutEnd,GetDate()), @LockoutEnabled,                    @AccessFailedCount,@Name,1)                      
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
select  @Id=ID from Users where NormalizedUserName=@Email      
end      
      
select RoleId from UserRoles where UserID=@Id      
end");
		}
    }
}

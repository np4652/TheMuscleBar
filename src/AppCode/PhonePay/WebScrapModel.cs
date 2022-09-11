using System.Collections.Generic;

namespace TheMuscleBar.AppCode.PhonePay
{
    public class WebScrapModel
    {
        #region Request
        public class RequestBase
        {
            public string token { get; set; }
            public string uuid { get; set; }
            public string devicefingerprint { get; set; }
        }

        public class Request : RequestBase
        {
            public string authtoken { get; set; }
            public string mobile { get; set; }
        }
        public class LoginOTPRequest : RequestBase
        {
            public string mobile { get; set; }
        }

        public class VerifyOTPRequest : LoginOTPRequest
        {
            public string otptoken { get; set; }
            public string otp { get; set; }
        }

        public class RefreshTokenRequest : Request
        {
            public string refreshtoken { get; set; }
        }

        public class GroupInfoRequest : Request
        {

        }
        public class UpdateSessionRequest : Request
        {
            public string usergroupid { get; set; }
        }

        public class MerchantProfileRequest : Request
        {

        }

        public class StoreListRequest : Request
        {
            public string merchantid { get; set; }
        }

        public class StoreWiseDataRequest : Request
        {
            public string storeid { get; set; }
        }

        public class QRPosListRequest : Request
        {
            public string storeid { get; set; }
            public string merchantid { get; set; }
        }

        public class QRDataByIdRequest : Request
        {
            public string qrid { get; set; }
        }

        public class TxnDataRequest : Request
        {
            public string merchantid { get; set; }
            public string fromdate { get; set; }//optional, default value is for today 00:00:00 ("2022-07-28 00:00:00")
            public string todate { get; set; } //"2022-08-02 13:59:59", //optional, default value is today 23:59:59 
            public List<string> status { get; set; } //["COMPLETED","ERRORED","CANCELLED","PENDING"], //optional
            public List<string> instruments { get; set; } //["ACCOUNT","WALLET","EXTERNAL_WALLET","CREDIT_CARD","DEBIT_CARD"]
        }

        public class GenrateAmountWiseQRRequest
        {
            public string QRID { get; set; }
            public string Displayname { get; set; }
            public decimal Amount { get; set; }
            public string TransactionNote { get; set; }
            public string MerchantTransactionId { get; set; }
        }

        #endregion Request
        #region Response
        public class PhonePayResponse
        {
            public string Resp_code { get; set; }
            public string Resp_desc { get; set; }
        }
        public class PhonePayResponse<T> : PhonePayResponse
        {
            
            public T data { get; set; }
        }

        public class LoginOTPResponse
        {
            public int expiry { get; set; }
            public string token { get; set; }
        }

        public class VerifyOTPResponse
        {
            public string phone { get; set; }
            public string userId { get; set; }
            public string encryptedToken { get; set; }
            public bool isSignUp { get; set; }
            public bool consent { get; set; }
            public string name { get; set; }
            public bool success { get; set; }
            public string token { get; set; }
            public string refreshToken { get; set; }
            public string @namespace { get; set; }
            public bool groupSelection { get; set; }
            public List<Group> groups { get; set; }
        }

        public class Group
        {
            public string userId { get; set; }
            public string groupName { get; set; }
            public string groupValue { get; set; }
            public string @namespace { get; set; }
            public int groupId { get; set; }
            public string status { get; set; }
            public bool active { get; set; }
            public UserRole userRole { get; set; }
        }

        public class UserRole
        {
            public string roleId { get; set; }
            public int userGroupId { get; set; }
            public bool active { get; set; }
            public object createdAt { get; set; }
            public object updatedAt { get; set; }
            public string createdBy { get; set; }
        }

        public class RefreshTokenResponse
        {
            public string code { get; set; }
            public string token { get; set; }
            public string encryptedToken { get; set; }
            public string refreshToken { get; set; }
            public string expiresAt { get; set; }
        }

        public class Address
        {
            public string building { get; set; }
            public string street { get; set; }
            public string locality { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string country { get; set; }
            public string pinCode { get; set; }
            public string lineOne { get; set; }
            public string lineTwo { get; set; }
        }

        public class GroupInfoResponse // group_infolist

        {
            public string merchantName { get; set; }
            public string roleName { get; set; }
            public Address address { get; set; }
            public Group userGroupNamespace { get; set; }
        }

        public class UpdateSessionResponse
        {
            public string phone { get; set; }
            public string groupName { get; set; }
            public string groupValue { get; set; }
            public string roleId { get; set; }
            public string roleName { get; set; }
            public string userId { get; set; }
            public List<object> userAccess { get; set; }
            public string encryptedToken { get; set; }
            public string name { get; set; }
            public bool success { get; set; }
            public string token { get; set; }
            public string refreshToken { get; set; }
            public string @namespace { get; set; }
            public bool groupSelection { get; set; }
            public List<Group> groups { get; set; }
        }

        public class Attribute
        {
            public string merchantId { get; set; }
            public string type { get; set; }
            public bool value { get; set; }
            public bool active { get; set; }
            public long createdAt { get; set; }
            public long updateAt { get; set; }
        }

        public class MerchantProfileResponse
        {
            public bool success { get; set; }
            public MerchantDetail data { get; set; }
        }

        public class MerchantDetail
        {
            public string merchantId { get; set; }
            public string fullName { get; set; }
            public string displayName { get; set; }
            public string type { get; set; }
            public string phoneNumber { get; set; }
            public string email { get; set; }
            public string pincode { get; set; }
            public long createdAt { get; set; }
            public List<Attribute> attributes { get; set; }
            public Address address { get; set; }
            public string superCategory { get; set; }
            public bool blacklisted { get; set; }
            public bool disabled { get; set; }
        }

        public class CategorySummary
        {
            public string categoryType { get; set; }
            public string categoryId { get; set; }
            public string categoryDisplayName { get; set; }
        }
        public class PageInfo
        {
            public bool hasNext { get; set; }
            public bool hasPrev { get; set; }
            public int currentPage { get; set; }
            public int total { get; set; }

        }
        public class StoreListingResponse
        {
            public string primaryImageId { get; set; }
            public string storeId { get; set; }
            public string displayName { get; set; }
            public string merchantId { get; set; }
            public List<CategorySummary> categorySummaries { get; set; }
            public string displayState { get; set; }
        }

        public class StoresSummary
        {
            public int totalStoreCount { get; set; }
        }
        public class StoreListResponse
        {
            public bool success { get; set; }
            public StoreListing data { get; set; }

        }
        public class StoreListing
        {
            public List<StoreListingResponse> storeListingResponses { get; set; }
            public PageInfo pageInfo { get; set; }
            public StoresSummary storesSummary { get; set; }
        }

        public class Location
        {
            public double lat { get; set; }
            public double lon { get; set; }
        }

        public class TIMING
        {
            public int openTime { get; set; }
            public int closeTime { get; set; }
        }

        public class AttributesMap
        {
            public List<TIMING> TIMING { get; set; }
        }
        public class StoreWiseDataResponse
        {
            public bool success { get; set; }
            public StroewwiseData data { get; set; }
        }

        public class StroewwiseData
        {
            public List<StoreDetailResponse> storeDetailResponses { get; set; }
            public PageInfo pageInfo { get; set; }
        }

        public class StoreDetailResponse//StoreWiseDataResponse
        {
            public string storeId { get; set; }
            public string name { get; set; }
            public string displayName { get; set; }
            public string phoneNumber { get; set; }
            public Address address { get; set; }
            public string merchantId { get; set; }
            public string shortLink { get; set; }
            public AttributesMap attributesMap { get; set; }
            public List<CategorySummary> categorySummaries { get; set; }
            public List<object> storeImages { get; set; }
            public Location location { get; set; }
            public string displayState { get; set; }
            public string shareTextMessage { get; set; }
        }

        public class Merchant
        {
            public string merchantId { get; set; }
            public string fullName { get; set; }
            public string displayName { get; set; }
            public bool disabled { get; set; }
            public long createdAt { get; set; }
        }

        public class NotificationReceiver
        {
            public string mode { get; set; }
            public int id { get; set; }
            public bool enabled { get; set; }
            public object createdAt { get; set; }
            public object updatedAt { get; set; }
            public string deviceId { get; set; }
            public string appId { get; set; }
            public string phoneNumber { get; set; }
        }
        public class Store
        {
            public string storeId { get; set; }
            public string merchantId { get; set; }
            public string name { get; set; }
            public string displayName { get; set; }
            public bool active { get; set; }
            public long createdAt { get; set; }
        }

        public class Terminal
        {
            public string terminalId { get; set; }
            public string storeId { get; set; }
            public string merchantId { get; set; }
            public string name { get; set; }
            public bool active { get; set; }
            public long createdAt { get; set; }
        }

        public class Mapping
        {
            public string merchantId { get; set; }
            public Merchant merchant { get; set; }
            public string storeId { get; set; }
            public Store store { get; set; }
            public string terminalId { get; set; }
            public Terminal terminal { get; set; }
            public List<NotificationReceiver> notificationReceiver { get; set; }
        }

        public class QRPOSListResponse
        {
            public int total { get; set; }
            public List<QRPOSList> data { get; set; }
        }

        public class QRPOSList
        {
            public string id { get; set; }
            public bool enabled { get; set; }
            public string state { get; set; }
            public long createdAt { get; set; }
            public Mapping mapping { get; set; }
        }

        public class QRDataByIdResponse
        {
            public string qrText { get; set; }
        }
        public class CustomerDetails
        {
            public string userId { get; set; }
            public string phoneNumber { get; set; }
            public string userName { get; set; }
        }

        public class InstrumentDetail
        {
            public string instrumentType { get; set; }
            public string displayName { get; set; }
            public int amount { get; set; }

        }

        public class MerchantDetails
        {
            public string merchantId { get; set; }
            public string storeId { get; set; }
            public string terminalId { get; set; }
            public string storeName { get; set; }
            public string terminalName { get; set; }
            public string qrCodeId { get; set; }
            public string merchantType { get; set; }
        }

        public class PaymentApp
        {
            public string paymentApp { get; set; }
            public string logo { get; set; }
            public string displayText { get; set; }
        }
        public class Settlement
        {
            public List<SettlementList> settlementList { get; set; }
            public string status { get; set; }
        }

        public class SettlementList
        {
            public string utr { get; set; }
            public string status { get; set; }
            public object settlementDate { get; set; }
            public int settlementAmount { get; set; }
        }
        public class TxnResult
        {
            public string unitId { get; set; }
            public string transactionId { get; set; }
            public string transactionType { get; set; }
            public string paymentState { get; set; }
            public int amount { get; set; }
            public string merchantTransactionId { get; set; }
            public string merchantOrderId { get; set; }
            public string providerReferenceId { get; set; }
            public List<InstrumentDetail> instrumentDetails { get; set; }
            public object transactionDate { get; set; }
            public MerchantDetails merchantDetails { get; set; }
            public CustomerDetails customerDetails { get; set; }
            public PaymentApp paymentApp { get; set; }
            public string utr { get; set; }
            public string vpa { get; set; }
            public string payResponseCode { get; set; }
            public string globalPaymentId { get; set; }
            public int offerAdjustment { get; set; }
            public string transactionNote { get; set; }
            public string errorMessage { get; set; }
            public bool cashbackApplied { get; set; }
            public List<object> vasDetails { get; set; }
            public List<object> refundTransactions { get; set; }
            public List<object> additionalTransactionDetails { get; set; }
            public bool externalVPATransaction { get; set; }
            public Settlement settlement { get; set; }
        }

        public class TxnDataResponse
        {
            public bool success { get; set; }
            public TxnData data { get; set; }
            public string errorCode { get; set; }
        }

        public class TxnData
        {
            public string errorCode { get; set; }
            public List<TxnResult> results { get; set; }
            public int totalResults { get; set; }
            public int totalAmount { get; set; }
        }
        #endregion Response
    }
}

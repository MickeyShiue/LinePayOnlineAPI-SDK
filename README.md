## LINE Pay Online API v3 C# SDK
非官方的 LINE Pay Online API v3 For C# SDK <br>
官方詳細文件 [點擊這裡](https://pay.line.me/jp/developers/apis/onlineApis?locale=zh_TW)

## nuget 套件
nuget套件位置：[LinePayOnlineAPIv3](https://www.nuget.org/packages/LinePayOnlineAPIv3/) <br>
nuget 指令：Install-Package LinePayOnlineAPIv3 -Version 1.0.0 <br>
dotnet CLI：dotnet add package LinePayOnlineAPIv3 --version 1.0.0


## 開始 Getting Started
建立 Sandbox [點擊這裡](https://pay.line.me/jp/developers/techsupport/sandbox/creation?locale=zh_TW)，註冊成功後會獲取一組 ChannelId、ChannelSecret <br>

## 環境 Environment
Sandbox：https://sandbox-api-pay.line.me <br>
Production：https://api-pay.line.me <br>

## 如何使用 SDK


呼叫端建立 LinePayClient
```csharp
var baseAddress = "https://sandbox-api-pay.line.me";
var channelId = "{ your ChannelId }";
var channelSecret = "{ your channelSecret }";
var client = new LinePayClient(baseAddress,channelId);
```


建立訂單交易相關資料
```csharp
private Reserve GetReserveData()
{
    Reserve reserve = new Reserve
    {
        Amount = 100,
        Currency = "TWD",
        OrderId = Guid.NewGuid().ToString()
    };

    List<Products> products = new List<Products>()
    {
        new Products()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "銅鑼燒",
            Quantity = 2,
            Price = 50
        }
    };

    reserve.Packages.Add
    (
        new Packages { Id = Guid.NewGuid().ToString(), Amount = 100, Products = products }
    );

    reserve.RedirectUrls.ConfirmUrl = "https://{ your Domain }/api/LinePay/confirm";
    reserve.RedirectUrls.CancelUrl = "https://{ your Domain }/api/LinePay/cancel";

    return reserve;
}
```

呼叫 Api
```csharp
[Route("reserve")]
public async Task<IActionResult> Reserve()
{
    var reserveData = GetReserveData();
    var nonce = Guid.NewGuid().ToString(); //時間戳記 or UUID 1~4
    var requestUrl = "/v3/payments/request";
    var requestJson = JsonConvert.SerializeObject(reserveData, client.SerializerSettings);
    var signature = client.GetSignature((channelSecret + requestUrl + requestJson + nonce), channelSecret);
  
    var result = await client.ReserveAsync(reserveData, nonce, signature);

    return Ok(result);
}
```

## 備註說明

* 因為 v3 版本要求每隻 api 都需要提供簽章，而簽章製作的官方說明如下

## Hmac Signature
* Algorithm : HMAC-SHA256
* Key : Channel Secret （LINE Pay商家中心提供"Channel Id"和"Channel SecretKey"）
* HTTP Method
  * GET : Channel Secret + URI + Query String + nonce
  * POST : Channel Secret + URI + Request Body + nonce
  
* HTTP Method : GET
  * Signature = Base64(HMAC-SHA256(Your ChannelSecret, (Your ChannelSecret + URI + Query String + nonce))) Query String : 不包含 " 問號（?）" 的Query String（例如：Name1=Value1&Name2=Value2...）
  
* HTTP Method : POST
  * Signature = Base64(HMAC-SHA256(Your ChannelSecret, (Your ChannelSecret + URI + RequestBody + nonce)))
 
* 我有提供 client.GetSignature(message,key);
  * message
   * Post：(channelSecret + Url + requestJson + nonce)
   * Get：(channelSecret + Url + queryString + nonce)
  * key：channelSecret
```csharp
var signature = client.GetSignature((channelSecret + url + queryString + nonce), channelSecret); //Get
var signature = client.GetSignature((channelSecret + url + requestJson + nonce), channelSecret); //Post
```

### [專案範例](https://github.com/MickeyShiue/LinePay/tree/master/LinePayEC.WebApi.DotNetFramework)

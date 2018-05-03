# Power BI Embedded for US Government cloud and embedding Q&A (for all tenant types)
This demonstrates how to use Power BI embedded in US Goverment cloud and how to embed Q&A. This is an extended sample of Microsoft's **[App Owns Data](https://github.com/Microsoft/PowerBI-Developer-Samples)**. 

## US Government power-bi embedded
1. Follow the steps described in https://docs.microsoft.com/en-us/power-bi/developer/embedding-content. I have listed more links in credits section to refer.
2. Make sure to register your app at **https://dev.powerbigov.us/apps**. Don't go to https://dev.powerbi.com/apps
3. You need to fill missing details in `web.config` before running the application


| Settings Key | Settings Value                                     |
| ------------ | -------------------------------------------------- |
| pbiUsername  | Your PowerBI Master User account                   |
| pbiPassword  | Your PowerBI Master User account password          |
| clientId     | Guid of Azure AD Native app                        |
| groupId      | Guid of Power BI Workspace. see the example below. |

![Group Id/Worksapce Id](https://github.com/kolluri-rk/PowerBI-Embedded-US-Government-Samples/blob/master/images-for-readme/groupid-worksapceid.PNG "Group Id/Worksapce Id")


4. You need to make sure to have below settings in `Cloud.config`. These are specific to `US Goverment cloud`.

| Settings Key | Settings Value                                               |
| ------------ | ------------------------------------------------------------ |
| authorityUrl | https://login.microsoftonline.com/common/oauth2/authorize/   |
| resourceUrl  | https://analysis.usgovcloudapi.net/powerbi/api               |
| apiUrl       | https://api.powerbigov.us/                                   |
| embedUrlBase | https://app.powerbigov.us                                    |

Microsoft has provided cloud configs for various tenant types in [App Owns Data](https://github.com/Microsoft/PowerBI-Developer-Samples/tree/master/App%20Owns%20Data/PowerBIEmbedded_AppOwnsData/CloudConfigs). If anyone other than Power BI Global and Power BI US Goverment looking at this sample, please check for your specific settings here. 

![cloudconfigs](https://github.com/kolluri-rk/PowerBI-Embedded-US-Government-Samples/blob/master/images-for-readme/power-bi-.PNG "cloud configs")



## Embedding Q&A (for all tenant types)

Find the demo here (http://cispowerbiembeddedapp.azurewebsites.net/Qna/EmbedQnA/Consumption)

PowerBI Client has Dashboards, Reports, Tiles, Datasets properties and each of these properties expose **GenerateTokenInGroupAsync** method to generate token. And each Dashboard/Report/Tile has an **EembedUrl** property, which is required for embedding. 

But PowerBI Client does not have Qnas property. Because, if you look at Generate token for Q&A (https://msdn.microsoft.com/library/mt784614.aspx#qanda), we need to generate token using Datasets property. All we are missing is, EmbedUrl for Qna. 

To expose Qnas property on **PowerBiClient** and EmbedUrl property on Qna, I created an extension for `PowerBIClient` in [PowerBIEmbedded.Extensions project](https://github.com/kolluri-rk/PowerBI-Embedded-US-Government-Samples/tree/master/App%20Owns%20Data/PowerBIEmbedded.Extensions). This gives similar api style as `Microsoft.PowerBI.Api`.


Gnerating token...

```
  // Get the qna in the group and dataset.
  // original power bi clinet does not have Qnas property. i built an extension in PowerBIEmbedded.Extensions project
  var qna = await client.Qnas().GetQnaInGroupAsync(GroupId, DatasetId);

  // Generate Embed Token.
  var generateTokenRequestParameters = new GenerateTokenRequest(accessLevel: "view");
  var tokenResponse = await client.Datasets.GenerateTokenInGroupAsync(GroupId, DatasetId, generateTokenRequestParameters);
```

Then, we can use qna.EmbedUrl

```
  // Generate Embed Configuration.
  var embedConfig = new QnAEmbedConfig()
  {
      EmbedToken = tokenResponse,
      EmbedUrl = qna.EmbedUrl,
      datasetId = DatasetId,
      viewMode = "Interactive",
      question = ""
  };
```


## Credits

[Madhu Chanthati](https://github.com/mchanthati) <br> 
Madhu is a Data Scientist who architected & designed datawarehouse, built & trained BI models, and created Power BI dashboards, reports, tiles and Qnas on governmnet cloud. 

Microsoft <br> 
https://github.com/Microsoft/PowerBI-Developer-Samples <br>

Reza Rad <br>
http://radacad.com/integrate-power-bi-into-your-application-part-1-register-your-app <br>
http://radacad.com/integrate-power-bi-into-your-application-part-2-authenticate <br>
http://radacad.com/integrate-power-bi-into-your-application-part-3-embed-content  <br>


## What's coming next

I will create a project to get embed token for Dashboards, Reports, Tiles, Datasets and Qnas using Azure functions. It will be handy for embedding Power BI in client side apps built using Angular or anyother client side frameworks.  

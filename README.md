# Vonage .NET Skeleton Application

[![Deploy to Azure](https://aka.ms/deploytoazurebutton)](https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fdev.azure.com%2Forgname%2Fprojectname%2F_apis%2Fgit%2Frepositories%2Freponame%2Fitems%3FscopePath%3D%2freponame%2fazuredeploy.json%26api-version%3D6.0)

This is a basic .NET core 3.1 application built to allow you to easily test your credentials and enviornment. Utilize this app to ensure that your API credentials are in working order and to make sure you can receive webhook data on your endpoint.

* [Requirements](#requirements)
* [Installation and Usage](#installation-and-usage)
  * [API Credentials](#api-credentials)
  * [Using ngrok](#using-ngrok)
  * [Running the Application](#running-the-application)
	* [Sending an Sms](#sending-an-sms)
	* [Receiving an Sms](#receiving-an-sms)
* [Contributing](#contributing)
* [License](#license)


## Requirements

This application requires you to have:

* [.NET core 3.1 runtime](https://dotnet.microsoft.com/download/dotnet-core/3.1)
* [Visual Studio 2019 - community should work](https://visualstudio.microsoft.com/downloads/)

## Installation and Usage

1. Grab this repo

```text
git clone https://github.com/nexmo/dotnet-skeleton-app.git
```

2. Open `dotnet-skeleton-app.sln` in Visual Studio

### Api Credentials

Your Api Key and Api Secret can be found in the [Vonage API Dashboard](https://dashboard.nexmo.com/)

You can set your Api Key or Api Secret for the app one of two ways:

1. In Visual Studio - right click on the dotnet-skeleton-app projects and click properties. Inside the properties menu click debug, in debug add new enviornment variables for `VONAGE_API_KEY` and `VONAGE_API_SECRET`

2. Open dotnet-skeleton-app/Controllers/SmsController.cs and replace the VONAGE_API_KEY and VONAGE_API_SECRET with your key/secret

### Using Ngrok

In order to test the incoming webhook data from Vonage, the Vonage API needs an externally accessible URL to send that data to. A commonly used service for development and testing is ngrok. The service will provide you with an externally available web address that creates a secure tunnel to your local environment. The [Nexmo Developer Platform](https://developer.nexmo.com/concepts/guides/testing-with-ngrok) has a guide to getting started with testing with ngrok.

Once you have your ngrok URL, you can enter your [Vonage Dashboard](https://dashboard.nexmo.com) and supply it as the `EVENT URL` for any Vonage service that sends event data via a webhook. A good test case is creating a Voice application and providing the ngrok URL in the following format as the event url:

`#{ngrok URL}/webhooks/inbound-sms`

You can then text your Vonage number, and with your skeleton application running you can observe the webhook data be received in real time for diagnosis of any issues and testing of your Vonage account.

## Running the Application

Once your API credentials have been added, and ngrok setup, you're ready, To start the application server simply hit f5 or click debug in IIS express

### Sending an SMS
This will bring you to the SMS controller page - here input

1. The number you are sending to
2. The Vonage Number you are sending from
3. The message you would like to send

After that click 'send' and your message will be sent

### Receiving an SMS

You can now send a sms message back to your Vonage number - since you've configured ngrok you will see something like this output to your debug console

```text
------------------------------------
INCOMING TEXT
From: 12018675309
To: 13218675309
Message: Test
Id: 170000026DFEE1C4
Time Stamp: 1585070409
------------------------------------
```

You can exit the application by hitting stop in visual studio.

## Contributing

We ❤️ contributions from everyone! [Bug reports](https://github.com/nexmo/dotnet-skeleton-app/issues), [bug fixes](https://github.com/nexmo/dotnet-skeleton-app/pulls) and feedback on the application is always appreciated. Look at the [Contributor Guidelines](https://github.com/Nexmo/dotnet-skeleton-app/blob/master/CONTRIBUTING.md) for more information and please follow the [GitHub Flow](https://guides.github.com/introduction/flow/index.html).

## License

This projet is under the [MIT License](LICENSE.md)
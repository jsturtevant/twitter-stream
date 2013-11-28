twitter-stream
=============

A light weight twitter streaming client and twitter client. It currently only implements [public statuses stream](https://dev.twitter.com/docs/api/1.1/post/statuses/filter). The public status stream matches one ore more filter predicates.


How to use
=============

1. Set up the authorization (found at your twitter dev account):

'''c#
string consumerkey =  "yourkey";
string consumersecret = "yourSecret";
string accessToken = "your access token";
string accessTokenSecret = "your tokent secret";

var oauthAuthorization = new OauthAuthorization(consumerkey, consumersecret,                                      accessToken, accessTokenSecret);
'''

2. Implement a processor.  There are examples located in twitter/processors.

'''c#
IProcessor processor = new GeoTweetCounter();
'''

3. Initialize a client and start the processing:

'''c#
var client = new TwitterStreamingClient(oauthAuthorization, processor);

client.StartStreaming("keyword");
Thread.Sleep(new TimeSpan(0, 0, seconds));
client.StopStreaming();
'''

Why
=============

Partially to learn how to sign http requests for [Oauth](https://dev.twitter.com/docs/auth/authorizing-request).  Mostly becuase existing library's did not set up for streaming Twitter's api easily.

Any feedback or questions is greatly appreicated.

License
=============
The MIT License (MIT)
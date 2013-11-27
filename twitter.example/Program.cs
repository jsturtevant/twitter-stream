namespace example
{
    using System;
    using System.Runtime.Remoting.Channels;
    using System.Threading;

    using audit.twitter;
    using audit.twitter.OAuth;
    using audit.twitter.Processors;

    using NLog;
    using NLog.Config;
    using NLog.Targets;

    using twitter.example.Properties;

    public enum AppType
    {
        geo,
        retweeter,
        unknown
    }

    class Program
    {
        static void Main(string[] args)
        {
            SetUpLogger();

            bool run = true;
            while (run)
            {
                Console.WriteLine("What would you like to stream?");
                string twitterKeyword = Console.ReadLine();

                Console.WriteLine("How Long to Listen for (seconds)?");
                int seconds = 10;
                int.TryParse(Console.ReadLine(), out seconds);

                Console.WriteLine("What would you like to run?");
                string appToRun = Console.ReadLine();

                AppType type = AppType.unknown;
                Enum.TryParse(appToRun, out type);

                ProcessorBase processor = null;
                switch (type)
                {
                    case AppType.geo:
                        processor = new GeoTweetCounter();
                        break;
                    case AppType.retweeter:
                        processor = new Retweeter(CurrentOAuth());
                        break;
                    default:
                        Console.WriteLine("please try again.");
                        break;
                }

                RunProcessor(processor, twitterKeyword, seconds);

                Console.WriteLine("Run again (y or n)?");
                string sendanother = Console.ReadLine();
                if (sendanother != "y")
                {
                    run = false;
                }
            }
        }
        
        private static void RunProcessor(ProcessorBase processor, string hash, int seconds)
        {
            OauthAuthorization oauthAuthorization = CurrentOAuth();
            var client = new TwitterStreamingClient(oauthAuthorization, processor);

            client.StartStreaming(hash);
            Thread.Sleep(new TimeSpan(0, 0, seconds));
            client.StopStreaming();

            processor.Report();
        }

        private static OauthAuthorization CurrentOAuth()
        {
            string consumerkey =  Settings.Default.key;
            string consumersecret = Settings.Default.secret;
            string accessToken = Settings.Default.accessToken;
            string accessTokenSecret = Settings.Default.accessTokenSecret;

            var oauthAuthorization = new OauthAuthorization(consumerkey, consumersecret, 
                                                            accessToken, accessTokenSecret);
            return oauthAuthorization;
        }

        private static void SetUpLogger()
        {
            // Step 1. Create configuration object 
            LoggingConfiguration config = new LoggingConfiguration();

            // Step 2. Create targets and add them to the configuration 
            ColoredConsoleTarget consoleTarget = new ColoredConsoleTarget();
            config.AddTarget("console", consoleTarget);

            FileTarget fileTarget = new FileTarget();
            config.AddTarget("file", fileTarget);

            // Step 3. Set target properties 
            consoleTarget.Layout = @"${date:format=HH\:MM\:ss} ${logger} ${message}";
            fileTarget.FileName = "${basedir}/log.txt";
            fileTarget.Layout = "${message}";

            // Step 4. Define rules
            LoggingRule rule1 = new LoggingRule("*", LogLevel.Info, consoleTarget);
            config.LoggingRules.Add(rule1);

            LoggingRule rule2 = new LoggingRule("*", LogLevel.Info, fileTarget);
            config.LoggingRules.Add(rule2);

            // Step 5. Activate the configuration
            LogManager.Configuration = config;
        }
    }
}

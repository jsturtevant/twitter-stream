namespace audit.twitter
{
    using System;

    using audit.twitter.DTO.Errors;

    public class TwitterClientException : Exception
    {
        public TwitterClientException(string message, TwitterErrors errors)
            : base(message)
        {
            this.Errors = errors;
        }

        public TwitterClientException(string message, TwitterClientException errors)
            : base(message, errors)
        {
            this.Errors = errors.Errors;
        }

        public TwitterClientException(string message)
            : base(message)
        {
            
        }

        public TwitterErrors Errors { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace BobTheBuilder.Tests
{
    internal class ImmutableBlogPost
    {
        private readonly string title;
        private readonly ImmutableAuthor author;
        private readonly string content;
        private readonly DateTime timestamp;
        private readonly IEnumerable<string> tags;

        public ImmutableBlogPost(string title, ImmutableAuthor author, string content, DateTime timestamp, IEnumerable<string> tags)
        {
            this.title = title;
            this.author = author;
            this.content = content;
            this.timestamp = timestamp;
            this.tags = tags;
        }

        public string Title
        {
            get { return title; }
        }

        public ImmutableAuthor Author
        {
            get { return author; }
        }

        public string Content
        {
            get { return content; }
        }

        public DateTime Timestamp
        {
            get { return timestamp; }
        }

        public IEnumerable<string> Tags
        {
            get { return tags; }
        }
    }
}

using System;
using System.Collections.Generic;

namespace BobTheBuilder.Tests
{
    internal class ImmutableBlogPost : IEquatable<ImmutableBlogPost>
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

        public override bool Equals(object obj)
        {
            return Equals(obj as ImmutableBlogPost);
        }

        public bool Equals(ImmutableBlogPost other)
        {
            return Equals(this, other);
        }

        public bool Equals(ImmutableBlogPost x, ImmutableBlogPost y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
            {
                return false;
            }

            var authorsAreEqual = x.Author.Equals(y.Author);
            var contentsAreEqual = x.Content == y.Content;
            var tagsAreEqual = x.Tags.Equals(y.Tags);
            var timestampsAreEqual = x.Timestamp == y.Timestamp;
            var titlesAreEqual = x.Title == y.Title;

            return authorsAreEqual &&
                   contentsAreEqual &&
                   tagsAreEqual &&
                   timestampsAreEqual &&
                   titlesAreEqual;
        }

        public override int GetHashCode()
        {
            return 1;
        }
    }
}

using identityWithChristina.Models;
using System;
using System.Collections.Generic;

namespace identityWithChristina.Models
{
    public class GeneralFeedback
    {
        public int FeedId { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; } = null!;
        public int Rate { get; set; }
        public DateTime Date { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;
    }
}

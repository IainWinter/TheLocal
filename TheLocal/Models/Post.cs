using System;

namespace TheLocal.Models {
    public struct Post {
        public string Title      { get; set; }
        public string Text       { get; set; }
        public string User         { get; set; }
        public DateTime Datetime { get; set; }
    }
}

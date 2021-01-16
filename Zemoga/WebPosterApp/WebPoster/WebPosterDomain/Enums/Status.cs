using System.ComponentModel;

namespace WebPosterDomain.Enums
{
    public enum Status
    {
        [Description("Post has been Created")]
        Pending = 1,

        [Description("Post data has been validated for publis")]
        Approved = 2,

        [Description("Post data has been rejected")]
        Rejected = 3,
    }
}

using System;
using System.Collections.Generic;

namespace CIPlatform.Entities.DataModels;

public partial class Notification
{
    public long NotificationId { get; set; }

    public string? NotificationText { get; set; }

    public int? ToUserId { get; set; }

    public string? NotificationType { get; set; }

    public int? FromId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Status { get; set; }

    public string? Avatar { get; set; }
}

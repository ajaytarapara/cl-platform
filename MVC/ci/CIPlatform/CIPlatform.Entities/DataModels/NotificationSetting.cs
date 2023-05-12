using System;
using System.Collections.Generic;

namespace CIPlatform.Entities.DataModels;

public partial class NotificationSetting
{
    public int UserId { get; set; }

    public bool? ApplicationApproval { get; set; }

    public bool? StoryApproval { get; set; }

    public bool? RecommandedFromMission { get; set; }

    public bool? RecommandedFromStory { get; set; }

    public bool? NewMissionAdded { get; set; }

    public int NotificationSettingId { get; set; }

    public bool? Receiveemailnotification { get; set; }
}

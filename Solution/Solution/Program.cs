/*
Right now, your application tracks the "Type" of a ticket with a data table related to the Ticket in question. 
It has been decided that this is not sufficient. All Tickets will share the same properties as those described in the original spec,
but each ticket will now belong to a special type, which will have its own special data.

The first two types are Bug Reports (which must track Error Codes and Error Logs, both strings),
and Service Requests, which have their own subset of Types of Requests (an Enum).
We can assume that more special types of tickets will be added later.
 */
public abstract class Ticket
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public TicketPriority Priority { get; set; }
    public TicketStatus Status { get; set; }
    public int ProjectId { get; set; }
    //public Project Project { get; set; }
    public string? DeveloperId { get; set; }
    //public ApplicationUser? Developer { get; set; }
    public string SubmitterId { get; set; }
    //public ApplicationUser Submitter { get; set; }
    //public ICollection<TicketHistory> TicketHistories { get; set; }
    //public ICollection<TicketComment> TicketComments { get; set; }
    //public ICollection<TicketNotification> TicketNotifications { get; set; }
    //public ICollection<TicketAttachment> TicketAttachments { get; set; }
    public Ticket()
    {
        //TicketHistories = new HashSet<TicketHistory>();
        //TicketComments = new HashSet<TicketComment>();
        //TicketNotifications = new HashSet<TicketNotification>();
        //TicketAttachments = new HashSet<TicketAttachment>();
    }
}
public class BugReport : Ticket //Bug Reports (which must track Error Codes and Error Logs, both strings)
{
    public List<string> ErrorCodes { get; set; }
    public List<string> ErrorLogs { get; set; }
}
public class ServiceRequest : Ticket //Service Requests, which have their own subset of Types of Requests (an Enum)
{
    public RequestType RequestType { get; set; }
}


public enum TicketPriority
{
    High,
    Medium,
    Low
};

public enum RequestType
{
    RequestType1,
    RequestType2,
    RequestType3,
};

public enum TicketStatus
{
    Unopened,
    Opened,
    OnHold,
    Completed
}

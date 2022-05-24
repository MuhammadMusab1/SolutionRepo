/*
Right now, your application tracks the "Type" of a ticket with a data table related to the Ticket in question. 
It has been decided that this is not sufficient. All Tickets will share the same properties as those described in the original spec,
but each ticket will now belong to a special type, which will have its own special data.

The first two types are Bug Reports (which must track Error Codes and Error Logs, both strings),
and Service Requests, which have their own subset of Types of Requests (an Enum).
We can assume that more special types of tickets will be added later.
 */
Ticket ticket = new BugReport();
ticket.CalculationContext.PerformBreachDeadlineCalculation(TicketPriority.High);
ticket = new WhiteGloveClient(ticket);
Console.WriteLine(ticket.BreachDeadline());
public abstract class Ticket
{
    public virtual int Id { get; set; }
    public virtual string Title { get; set; }
    public virtual string Description { get; set; }
    public virtual DateTime CreatedDate { get; set; }
    public virtual DateTime UpdatedDate { get; set; }
    public virtual TicketPriority Priority { get; set; }
    public virtual TicketStatus Status { get; set; }
    public virtual int ProjectId { get; set; }
    //public Project Project { get; set; }
    public virtual string? DeveloperId { get; set; }
    //public ApplicationUser? Developer { get; set; }
    public virtual string SubmitterId { get; set; }
    public virtual ServiceLevelAgreement ServiceLevelAgreement { get; set; }
    public virtual double ResponseDeadline()
    {
        return ServiceLevelAgreement.ResponseDeadline;
    }
    public virtual double BreachDeadline()
    {
        return ServiceLevelAgreement.BreachDeadline;
    }
    public virtual CalculationContext CalculationContext { get; set; }
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
    public BugReport()
    {
        CalculationContext = new BugReportContext();
    }
}
public class ServiceRequest : Ticket //Service Requests, which have their own subset of Types of Requests (an Enum)
{
    public RequestType RequestType { get; set; }
    public ServiceRequest()
    {
        CalculationContext = new ServiceRequestContext();
    }
}
//Strategies
public interface ICalculationStrategy 
{
    public void CalculateResponseDeadline(TicketPriority priority);
    public void CalculateBreachDeadline(TicketPriority priority);
}
public class BugReportCalculationStrategy : ICalculationStrategy
{
    public void CalculateResponseDeadline(TicketPriority priority)
    {
        //implement accordingly
        //For example, a High Priority ticket might have a base Response Deadline of 1 hour, and for a Bug Report, the multiplier is 2: 
        //the Response Deadline is calculated, then, to 2 hours.
    }
    public void CalculateBreachDeadline(TicketPriority priority)
    {
        //implement accordingly
    }
}
public class ServiceRequestCalculationStrategy : ICalculationStrategy
{
    public void CalculateResponseDeadline(TicketPriority priority)
    {
        //implement accordingly
    }
    public void CalculateBreachDeadline(TicketPriority priority)
    {
        //implement accordingly
    }
}
//Context
public abstract class CalculationContext
{
    public ICalculationStrategy CalculationStrategy { get; set; }
    public TicketPriority Priority { get; set; }
    public void PerformResponseDeadlineCalculation(TicketPriority priority)
    {
        CalculationStrategy.CalculateResponseDeadline(priority);
    }
    public void PerformBreachDeadlineCalculation(TicketPriority priority)
    {
        CalculationStrategy.CalculateBreachDeadline(priority);
    }
}
public class ServiceRequestContext : CalculationContext
{
    public ServiceRequestContext()
    {
        CalculationStrategy = new ServiceRequestCalculationStrategy();
    }
}
public class BugReportContext : CalculationContext
{
    public BugReportContext()
    {
        CalculationStrategy = new BugReportCalculationStrategy();
    }
}

/*
2.
Each ticket now needs to track a Service Level Agreement, which works like a set deadline. 
This consists of two properties, a response deadline (when the ticket needs to be assigned) 
and a breach deadline (when the ticket should be resolved by). 
Each deadline will be formatted in an amount of hours, stored in an integer.

Your project manager expects that you will need to use a Design Pattern to implement this change.

These properties will need to be calculated separately by the system 

whenever a new ticket is created. Each different type of ticket will apply a base calculation with the Priority,
and multiplied by a different amount depending on the ticket type. 

For example, a High Priority ticket might have a base Response Deadline of 1 hour, and for a Bug Report, the multiplier is 2: 
the Response Deadline is calculated, then, to 2 hours.

Both the Breach Deadline and Response Deadlines will be modified by a number of different factors that can be added to the ticket.
 If, for example, the "White Glove Client" Modifier is added to the ticket, it will multiple the available hours by 0.8.
 If the "Backlog Reissue" modifier is added to the ticket, the Breach Deadline has 100 hours added to it.
 We should be able to add any number of modifiers to a ticket when it is created.
 */
public class ServiceLevelAgreement
{
    public double ResponseDeadline { get; set; }
    public double BreachDeadline { get; set; }
}
public abstract class Modifier : Ticket
{
    public Ticket Ticket { get; set; }
    public override TicketPriority Priority 
    {
        get { return Ticket.Priority; }
        set { Ticket.Priority = value; }
    }
    public override ServiceLevelAgreement ServiceLevelAgreement 
    { 
        get { return Ticket.ServiceLevelAgreement; }
        set { Ticket.ServiceLevelAgreement = value; }
    }
    //do same for other properties

}
public class WhiteGloveClient : Modifier
{
    public WhiteGloveClient(Ticket ticket)
    {
        Ticket = ticket;
    }
    public override double ResponseDeadline()
    {
        return Ticket.ResponseDeadline() * 0.8;
    }
    public override double BreachDeadline()
    {
        return Ticket.BreachDeadline() * 0.8;
    }
}
public class BacklogReissue : Modifier
{
    public BacklogReissue(Ticket ticket)
    {
        Ticket = ticket;
    }
    public override double BreachDeadline()
    {
        return Ticket.BreachDeadline() + 100;
    }
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

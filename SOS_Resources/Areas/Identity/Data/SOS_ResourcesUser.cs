using Microsoft.AspNetCore.Identity;

namespace SOS_Resources.Areas.Identity.Data;

public class SOS_User : IdentityUser
{
    [PersonalData]
    public string? HendrixID { get; set; }
    [PersonalData]
    public string? FName { get; set; }
    [PersonalData]
    public string? LName { get; set; }
    [PersonalData]
    public string? UserEmail { get; set; }
    [PersonalData]
    public string? UserPhoneNum { get; set; }

    [PersonalData]
    public string? Pronouns { get; set; }
    [PersonalData]
    public string? PrefName { get; set; }
    
    public string DisplayName
    {
        get
        {
            if (!String.IsNullOrEmpty(PrefName)){
                return PrefName +" "+ LName;
            } else {
                return FName +" "+ LName;
            }
        }
    }

    [PersonalData]
    public string? Class { get; set; }
    [PersonalData]
    public string? CampusAdd { get; set; } 
    [PersonalData]
    public string? CampusEmail { get; set; }
    
    

    [PersonalData]
    public string? EmergFName { get; set; }
    [PersonalData]
    public string? EmergLName { get; set; }
    [PersonalData]
    public string? EmergEmail { get; set; }
    [PersonalData]
    public string? EmergRelation { get; set; }
    [PersonalData]
    public string? EmergPhone { get; set; }

    [PersonalData]
    public string? Employer { get; set; }
    [PersonalData]
    public string? EmployerPhone { get; set; }
    [PersonalData]
    public string? JobPosition { get; set; }
    [PersonalData]
    public string? PayType { get; set; }
    [PersonalData]
    public string? PayFreq { get; set; }
    [PersonalData]
    public int MonthlyWages { get; set; }

    //Need to add in Financial Aid Statment Datatype but I'm not sure about it right now

    [PersonalData]
    public string? ReferredBy { get; set; }

}
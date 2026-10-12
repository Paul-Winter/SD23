namespace ZooStav.Wolf.Web.Models;

public enum DiaryEntryType
{
    Feeding = 1,      
    Vaccination = 2,   
    Mating = 3,       
    Offspring = 4,      
    Illness = 5,       
    Treatment = 6,     
    Observation = 7,    
    Weighing = 8,       
    Examination = 9,    
    Relocation = 10,    
    Comment = 11       
}

public static class DiaryEntryTypeNames
{
    public static string ToRussian(this DiaryEntryType type) => type switch
    {
        DiaryEntryType.Feeding => "Кормёжка",
        DiaryEntryType.Vaccination => "Вакцинация",
        DiaryEntryType.Mating => "Спаривание",
        DiaryEntryType.Offspring => "Потомство",
        DiaryEntryType.Illness => "Болезнь",
        DiaryEntryType.Treatment => "Лечение",
        DiaryEntryType.Observation => "Наблюдение",
        DiaryEntryType.Weighing => "Взвешивание",
        DiaryEntryType.Examination => "Осмотр",
        DiaryEntryType.Relocation => "Перемещение",
        DiaryEntryType.Comment => "Комментарий",
        _ => type.ToString()
    };

    public static string CssClass(this DiaryEntryType type) => type switch
    {
        DiaryEntryType.Feeding => "tag tag-feeding",
        DiaryEntryType.Vaccination => "tag tag-vaccination",
        DiaryEntryType.Mating => "tag tag-mating",
        DiaryEntryType.Offspring => "tag tag-offspring",
        DiaryEntryType.Illness => "tag tag-illness",
        DiaryEntryType.Treatment => "tag tag-treatment",
        DiaryEntryType.Weighing => "tag tag-weighing",
        DiaryEntryType.Examination => "tag tag-examination",
        DiaryEntryType.Relocation => "tag tag-relocation",
        _ => "tag tag-observation"
    };
}

public static class Roles
{
    public const string Admin = "Admin";                 
    public const string Keeper = "Keeper";               
    public const string Veterinarian = "Veterinarian"; 
    public const string Observer = "Observer";          

    public static readonly string[] Employees = { Admin, Keeper, Veterinarian, Observer };
    public static readonly string[] DiaryEditors = { Admin, Keeper, Veterinarian };
    public static readonly string[] MedicalStaff = { Admin, Veterinarian };

    public static string ToRussian(string role) => role switch
    {
        Admin => "Администратор (руководство)",
        Keeper => "Кипер",
        Veterinarian => "Ветеринар",
        Observer => "Наблюдатель (чтение)",
        _ => role
    };
}

public static class MediaKinds
{
    public const string Photo = "photo";
    public const string Video = "video";
    public const string Webcam = "webcam";
}

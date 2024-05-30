namespace TrashGrounds.User.Services.Configs;

public static class PasswordConfig
{
    public const int MinimumLength = 8;
    
    public const bool RequireNonAlphanumeric = false;   // требуются ли не алфавитно-цифровые символы
    
    public const bool RequireLowercase = true; // требуются ли символы в нижнем регистре
    
    public const bool RequireUppercase = true; // требуются ли символы в верхнем регистре
    
    public const bool RequireDigit = true; // требуются ли цифры
}
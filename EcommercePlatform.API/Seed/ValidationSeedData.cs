using EcommercePlatform.API.Models;

namespace EcommercePlatform.API.Seed;

public static class ValidationSeedData
{
    public static List<ValidationSetting> GetShippingValidationSettings()
    {
        return new List<ValidationSetting>
        {
            New("Shipping", "fullName", new ValidationRules { Required = true, MinLength = 2, MaxLength = 100, RegexPattern = "^[a-zA-Z .'-]+$" },
                new ErrorMessages { Required = "Full name is required", MinLength = "Name must be at least 2 characters", MaxLength = "Name must not exceed 100 characters", Pattern = "Name can only contain letters, spaces, dots, apostrophes and hyphens" }),

            New("Shipping", "email", new ValidationRules { Required = true, MaxLength = 100, RegexPattern = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$" },
                new ErrorMessages { Required = "Email is required", MaxLength = "Email must not exceed 100 characters", Pattern = "Enter a valid email address" }),

            New("Shipping", "phone", new ValidationRules { Required = true, MaxLength = 10, RegexPattern = "^[6-9]\\d{9}$" },
                new ErrorMessages { Required = "Phone number is required", MaxLength = "Phone number must not exceed 10 digits", Pattern = "Enter a valid 10-digit Indian mobile number" }),

            New("Shipping", "address", new ValidationRules { Required = true, MinLength = 5, MaxLength = 200 },
                new ErrorMessages { Required = "Address is required", MinLength = "Address must be at least 5 characters", MaxLength = "Address must not exceed 200 characters" }),

            New("Shipping", "city", new ValidationRules { Required = true, MaxLength = 50, RegexPattern = "^[a-zA-Z .'-]+$" },
                new ErrorMessages { Required = "City is required", MaxLength = "City must not exceed 50 characters", Pattern = "City can only contain letters and spaces" }),

            New("Shipping", "district", new ValidationRules { Required = true, MaxLength = 50, RegexPattern = "^[a-zA-Z .'-]+$" },
                new ErrorMessages { Required = "District is required", MaxLength = "District must not exceed 50 characters", Pattern = "District can only contain letters and spaces" }),

            New("Shipping", "state", new ValidationRules { Required = true, MaxLength = 50, RegexPattern = "^[a-zA-Z .'-]+$" },
                new ErrorMessages { Required = "State is required", MaxLength = "State must not exceed 50 characters", Pattern = "State can only contain letters and spaces" }),

            New("Shipping", "zipCode", new ValidationRules { Required = true, RegexPattern = "^\\d{6}$" },
                new ErrorMessages { Required = "PIN code is required", Pattern = "PIN code must be exactly 6 digits" }),

            New("Shipping", "country", new ValidationRules { Required = true, MaxLength = 50 },
                new ErrorMessages { Required = "Country is required", MaxLength = "Country must not exceed 50 characters" }),

            New("Shipping", "landmark", new ValidationRules { MaxLength = 100 },
                new ErrorMessages { MaxLength = "Landmark must not exceed 100 characters" }),

            New("Shipping", "buildingName", new ValidationRules { MaxLength = 100 },
                new ErrorMessages { MaxLength = "Building name must not exceed 100 characters" }),

            New("Shipping", "floorUnit", new ValidationRules { MaxLength = 50 },
                new ErrorMessages { MaxLength = "Floor/Unit must not exceed 50 characters" }),

            New("Shipping", "alternatePhone", new ValidationRules { MaxLength = 10, RegexPattern = "^[6-9]\\d{9}$" },
                new ErrorMessages { MaxLength = "Alternate phone must not exceed 10 digits", Pattern = "Enter a valid 10-digit Indian mobile number" }),
        };
    }

    public static List<ValidationSetting> GetBillingValidationSettings()
    {
        return new List<ValidationSetting>
        {
            // Card payment fields
            New("Billing", "cardName", new ValidationRules { Required = true, MinLength = 2, MaxLength = 100, RegexPattern = "^[a-zA-Z .'-]+$" },
                new ErrorMessages { Required = "Name on card is required", MinLength = "Name must be at least 2 characters", MaxLength = "Name must not exceed 100 characters", Pattern = "Name can only contain letters and spaces" }),

            New("Billing", "cardNumber", new ValidationRules { Required = true, RegexPattern = "^[\\d\\s]{13,19}$" },
                new ErrorMessages { Required = "Card number is required", Pattern = "Card number must be 13 to 19 digits" }),

            New("Billing", "expiryDate", new ValidationRules { Required = true, RegexPattern = "^(0[1-9]|1[0-2])\\/\\d{2}$" },
                new ErrorMessages { Required = "Expiry date is required", Pattern = "Expiry must be in MM/YY format" }),

            New("Billing", "cvv", new ValidationRules { Required = true, RegexPattern = "^\\d{3,4}$" },
                new ErrorMessages { Required = "CVV is required", Pattern = "CVV must be 3 or 4 digits" }),
        };
    }

    public static List<ValidationSetting> GetBillingAddressValidationSettings()
    {
        return new List<ValidationSetting>
        {
            // Billing address fields
            New("BillingAddress", "billingFullName", new ValidationRules { Required = true, MinLength = 2, MaxLength = 100, RegexPattern = "^[a-zA-Z .'-]+$" },
                new ErrorMessages { Required = "Full name is required", MinLength = "Name must be at least 2 characters", MaxLength = "Name must not exceed 100 characters", Pattern = "Name can only contain letters, spaces, dots, apostrophes and hyphens" }),

            New("BillingAddress", "billingPhone", new ValidationRules { Required = true, MaxLength = 10, RegexPattern = "^[6-9]\\d{9}$" },
                new ErrorMessages { Required = "Phone number is required", MaxLength = "Phone number must not exceed 10 digits", Pattern = "Enter a valid 10-digit Indian mobile number" }),

            New("BillingAddress", "billingAddress", new ValidationRules { Required = true, MinLength = 5, MaxLength = 200 },
                new ErrorMessages { Required = "Address is required", MinLength = "Address must be at least 5 characters", MaxLength = "Address must not exceed 200 characters" }),

            New("BillingAddress", "billingCity", new ValidationRules { Required = true, MaxLength = 50, RegexPattern = "^[a-zA-Z .'-]+$" },
                new ErrorMessages { Required = "City is required", MaxLength = "City must not exceed 50 characters", Pattern = "City can only contain letters and spaces" }),

            New("BillingAddress", "billingDistrict", new ValidationRules { Required = true, MaxLength = 50, RegexPattern = "^[a-zA-Z .'-]+$" },
                new ErrorMessages { Required = "District is required", MaxLength = "District must not exceed 50 characters", Pattern = "District can only contain letters and spaces" }),

            New("BillingAddress", "billingState", new ValidationRules { Required = true, MaxLength = 50, RegexPattern = "^[a-zA-Z .'-]+$" },
                new ErrorMessages { Required = "State is required", MaxLength = "State must not exceed 50 characters", Pattern = "State can only contain letters and spaces" }),

            New("BillingAddress", "billingZipCode", new ValidationRules { Required = true, RegexPattern = "^\\d{6}$" },
                new ErrorMessages { Required = "PIN code is required", Pattern = "PIN code must be exactly 6 digits" }),

            New("BillingAddress", "billingCountry", new ValidationRules { Required = true, MaxLength = 50 },
                new ErrorMessages { Required = "Country is required", MaxLength = "Country must not exceed 50 characters" }),

            New("BillingAddress", "billingLandmark", new ValidationRules { MaxLength = 100 },
                new ErrorMessages { MaxLength = "Landmark must not exceed 100 characters" }),

            New("BillingAddress", "billingBuildingName", new ValidationRules { MaxLength = 100 },
                new ErrorMessages { MaxLength = "Building name must not exceed 100 characters" }),

            New("BillingAddress", "billingFloorUnit", new ValidationRules { MaxLength = 50 },
                new ErrorMessages { MaxLength = "Floor/Unit must not exceed 50 characters" }),

            New("BillingAddress", "billingAlternatePhone", new ValidationRules { MaxLength = 10, RegexPattern = "^[6-9]\\d{9}$" },
                new ErrorMessages { MaxLength = "Alternate phone must not exceed 10 digits", Pattern = "Enter a valid 10-digit Indian mobile number" }),
        };
    }

    public static List<ValidationSetting> GetAllSettings()
    {
        var all = new List<ValidationSetting>();
        all.AddRange(GetShippingValidationSettings());
        all.AddRange(GetBillingValidationSettings());
        all.AddRange(GetBillingAddressValidationSettings());
        return all;
    }

    private static ValidationSetting New(string entityType, string fieldName, ValidationRules rules, ErrorMessages messages)
    {
        return new ValidationSetting
        {
            Id = Guid.NewGuid().ToString(),
            EntityType = entityType,
            FieldName = fieldName,
            ValidationRules = rules,
            ErrorMessages = messages,
            IsActive = true,
        };
    }
}

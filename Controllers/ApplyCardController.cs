using System.Diagnostics;
using CC_Regist_System.Database;
using CC_Regist_System.Enums;
using CC_Regist_System.Models;
using CC_Regist_System.Models.Address;
using CC_Regist_System.Models.Cards;
using CC_Regist_System.Models.Users;
using CC_Regist_System.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CC_Regist_System.Controllers;

public class ApplyCardController : Controller
{
    private readonly ILogger<ApplyCardController> _logger;
    private readonly ApplicationDBContext _context;

    public ApplyCardController(ILogger<ApplyCardController> logger, ApplicationDBContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public IActionResult Apply()
    {
        ViewData["IsRegisterCardPage"] = true;

        var user = _context.Users
        .Include(u => u.UserDetail)
            .ThenInclude(ud => ud.Address)
                .ThenInclude(a => a.City)
        .Include(u => u.UserDetail)
            .ThenInclude(ud => ud.Address)
                .ThenInclude(a => a.Province)
        .Include(u => u.UserDetail)
            .ThenInclude(ud => ud.Address)
                .ThenInclude(a => a.Country)
        .Include(u => u.Card)
            .ThenInclude(c => c.CardDetail)
        .FirstOrDefault(u => u.Username == HttpContext.Session.GetString("Username"));

        ViewData["IDCardNumber"] = HttpContext.Session.GetString("IDCardNumber") ?? user?.UserDetail?.IDNumber ?? "";
        ViewData["DateOfBirthDay"] = HttpContext.Session.GetString("DateOfBirthDay") ?? user?.UserDetail?.DateOfBirthDay.ToString("yyyy-MM-dd") ?? "";
        ViewData["Gender"] = HttpContext.Session.GetString("Gender") ?? user?.UserDetail?.Gender ?? "";
        ViewData["MaritalStatus"] = HttpContext.Session.GetString("MaritalStatus") ?? user?.UserDetail?.MaritalStatus ?? "";
        ViewData["City"] = HttpContext.Session.GetString("City") ?? user?.UserDetail?.Address?.City?.CityName ?? "";
        ViewData["Province"] = HttpContext.Session.GetString("Province") ?? user?.UserDetail?.Address?.Province?.ProvinceName ?? "";
        ViewData["Country"] = HttpContext.Session.GetString("Country") ?? user?.UserDetail?.Address?.Country?.CountryName ?? "";
        ViewData["Address"] = HttpContext.Session.GetString("Address") ?? user?.UserDetail?.Address?.DetailAddress ?? "";
        ViewData["CompanyName"] = HttpContext.Session.GetString("CompanyName") ?? user?.UserDetail?.CompanyName ?? "";
        ViewData["Position"] = HttpContext.Session.GetString("Position") ?? user?.UserDetail?.Position ?? "";
        ViewData["Income"] = HttpContext.Session.GetString("Income") ?? user?.UserDetail?.MonthlyIncome.ToString() ?? "";
        ViewData["BankAccountNumber"] = HttpContext.Session.GetString("BankAccountNumber") ?? user?.UserDetail?.BankAccountNumber.ToString() ?? "";
        ViewData["ScanID"] = HttpContext.Session.GetString("ScanID") ?? "";
        ViewData["SalarySlip"] = HttpContext.Session.GetString("SalarySlip") ?? "";
        ViewData["CardName"] = HttpContext.Session.GetString("CardName") ?? user?.Card?.CardName ?? "";
        ViewData["CardType"] = HttpContext.Session.GetString("CardType") ?? user?.Card?.CardType ?? "";
        ViewData["CardTier"] = HttpContext.Session.GetString("CardTier") ?? user?.Card?.CardTier ?? "";
        ViewData["CreditLimit"] = HttpContext.Session.GetString("CreditLimit") ?? user?.Card?.CardDetail?.CreditLimit.ToString() ?? "";
        ViewData["InterestRate"] = HttpContext.Session.GetString("InterestRate") ?? user?.Card?.CardDetail?.InterestRate.ToString() ?? "";
        ViewData["AnnualFee"] = HttpContext.Session.GetString("AnnualFee") ?? user?.Card?.CardDetail?.AnnualFee.ToString() ?? "";

        var model = new ApplyCardViewModel
        {
            CardNameOptions = Enum.GetValues<CardNameEnum>()
                .Select(cn => new SelectListItem
                {
                    Value = cn.ToString(),
                    Text = cn.GetEnumDescription(),
                })
                .ToList(),

            GenderOptions = Enum.GetValues<GenderEnum>()
                .Select(gender => new SelectListItem
                {
                    Value = gender.ToString(),
                    Text = gender.ToString(),
                })
                .ToList(),

            MaritalStatusOptions = Enum.GetValues<MaritalStatusEnum>()
                .Select(ms => new SelectListItem
                {
                    Value = ms.ToString(),
                    Text = ms.ToString(),
                    Selected = false,
                })
                .ToList(),

            User = user ?? new UsersModel(),
        };

        return View(model);
    }

    [HttpGet]
    public IActionResult Review()
    {
        ViewData["IsReview"] = true;

        var user = _context.Users
            .Include(u => u.UserDetail)
            .Include(u => u.Card)
            .ThenInclude(c => c.CardDetail)
            .FirstOrDefault(u => u.Username == HttpContext.Session.GetString("Username"));

        ViewData["IDCardNumber"] = HttpContext.Session.GetString("IDCardNumber") ?? user?.UserDetail?.IDNumber ?? "";
        ViewData["DateOfBirthDay"] = HttpContext.Session.GetString("DateOfBirthDay") ?? user?.UserDetail?.DateOfBirthDay.ToString("yyyy-MM-dd") ?? "";
        ViewData["Gender"] = HttpContext.Session.GetString("Gender") ?? user?.UserDetail?.Gender ?? "";
        ViewData["MaritalStatus"] = HttpContext.Session.GetString("MaritalStatus") ?? user?.UserDetail?.MaritalStatus ?? "";
        ViewData["City"] = HttpContext.Session.GetString("City") ?? user?.UserDetail?.Address?.City?.CityName ?? "";
        ViewData["Province"] = HttpContext.Session.GetString("Province") ?? user?.UserDetail?.Address?.Province?.ProvinceName ?? "";
        ViewData["Country"] = HttpContext.Session.GetString("Country") ?? user?.UserDetail?.Address?.Country?.CountryName ?? "";
        ViewData["Address"] = HttpContext.Session.GetString("Address") ?? user?.UserDetail?.Address?.DetailAddress ?? "";
        ViewData["CompanyName"] = HttpContext.Session.GetString("CompanyName") ?? user?.UserDetail?.CompanyName ?? "";
        ViewData["Position"] = HttpContext.Session.GetString("Position") ?? user?.UserDetail?.Position ?? "";
        ViewData["Income"] = HttpContext.Session.GetString("Income") ?? user?.UserDetail?.MonthlyIncome.ToString() ?? "";
        ViewData["BankAccountNumber"] = HttpContext.Session.GetString("BankAccountNumber") ?? user?.UserDetail?.BankAccountNumber.ToString() ?? "";
        ViewData["ScanIDFileName"] = HttpContext.Session.GetString("ScanIDFileName") ?? "";
        ViewData["CardName"] = HttpContext.Session.GetString("CardName") ?? user?.Card?.CardName ?? "";
        ViewData["CardType"] = HttpContext.Session.GetString("CardType") ?? user?.Card?.CardType ?? "";
        ViewData["CardTier"] = HttpContext.Session.GetString("CardTier") ?? user?.Card?.CardTier ?? "";
        ViewData["CreditLimit"] = HttpContext.Session.GetString("CreditLimit") ?? user?.Card?.CardDetail?.CreditLimit.ToString() ?? "";
        ViewData["InterestRate"] = HttpContext.Session.GetString("InterestRate") ?? user?.Card?.CardDetail?.InterestRate.ToString() ?? "";
        ViewData["AnnualFee"] = HttpContext.Session.GetString("AnnualFee") ?? user?.Card?.CardDetail?.AnnualFee.ToString() ?? "";

        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> ReviewApplyCard(ApplyCardViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        HttpContext.Session.SetString("IDCardNumber", model.IDCardNumber?.ToString() ?? "");
        HttpContext.Session.SetString("DateOfBirthDay", model.DateOfBirthDay.ToString("yyyy-MM-dd") ?? "");
        HttpContext.Session.SetString("Gender", model.Gender?.ToString() ?? "");
        HttpContext.Session.SetString("MaritalStatus", model.MaritalStatus?.ToString() ?? "");
        HttpContext.Session.SetString("City", model.City ?? "");
        HttpContext.Session.SetString("Province", model.Province ?? "");
        HttpContext.Session.SetString("Country", model.Country ?? "");
        HttpContext.Session.SetString("Address", model.Address ?? "");
        HttpContext.Session.SetString("CompanyName", model.CompanyName ?? "");
        HttpContext.Session.SetString("Position", model.Position ?? "");
        HttpContext.Session.SetString("Income", model.Income.ToString() ?? "0");
        HttpContext.Session.SetString("BankAccountNumber", model.BankAccountNumber ?? "");
        HttpContext.Session.SetString("CardName", model.CardName?.ToString() ?? "");
        HttpContext.Session.SetString("CardType", model.CardType?.ToString() ?? "");
        HttpContext.Session.SetString("CardTier", model.CardTier?.ToString() ?? "");
        HttpContext.Session.SetString("CreditLimit", model.CreditLimit.ToString() ?? "0");
        HttpContext.Session.SetString("InterestRate", model.InterestRate.ToString() ?? "0.0");
        HttpContext.Session.SetString("AnnualFee", model.AnnualFee.ToString() ?? "0.0");

        if (model.ScanID != null && model.ScanID.Length > 0)
        {
            using (var memoryStream = new MemoryStream())
            {
                await model.ScanID.CopyToAsync(memoryStream);
                HttpContext.Session.Set("ScanIDFileBytes", memoryStream.ToArray());
            }
            HttpContext.Session.SetString("ScanIDFileName", model.ScanID.FileName);
        }

        return RedirectToAction("Review", "ApplyCard");
    }

    [HttpPost]
    public async Task<IActionResult> ApplyCardRegister()
    {
        var user = _context.Users.FirstOrDefault(u => u.UserID == HttpContext.Session.GetInt32("UserID"));

        if (user == null)
        {
            return BadRequest(new { message = "User not found." });
        }

        if (await _context.Cards.AnyAsync(c => c.UserID == user.UserID))
        {
            return BadRequest(new { message = "You already have a credit card." });
        }


        double.TryParse(HttpContext.Session.GetString("Income"), out double income);
        DateOnly.TryParse(HttpContext.Session.GetString("DateOfBirthDay"), out DateOnly birthDate);

        var userDetail = new UserDetailsModel
        {
            UserID = user.UserID,
            DateOfBirthDay = birthDate,
            IDNumber = HttpContext.Session.GetString("IDCardNumber"),
            MaritalStatus = HttpContext.Session.GetString("MaritalStatus"),
            CompanyName = HttpContext.Session.GetString("CompanyName"),
            Position = HttpContext.Session.GetString("Position"),
            BankAccountNumber = HttpContext.Session.GetString("BankAccountNumber"),
            Gender = HttpContext.Session.GetString("Gender"),
            MonthlyIncome = income,
        };

        var existingUserDetail = await _context.UserDetails
            .AsTracking()
            .FirstOrDefaultAsync(ud => ud.UserID == user.UserID);


        if (existingUserDetail != null)
        {
            foreach (var prop in typeof(UserDetailsModel).GetProperties())
            {
                if (prop.Name == nameof(UserDetailsModel.UserDetailID) || prop.Name == nameof(UserDetailsModel.UserID) || prop.Name == nameof(UserDetailsModel.CreatedAt))
                    continue;

                if (prop.DeclaringType != typeof(UserDetailsModel)) continue;

                var newValue = prop.GetValue(userDetail);
                var oldValue = prop.GetValue(existingUserDetail);


                if (!object.Equals(newValue, oldValue))
                {
                    prop.SetValue(existingUserDetail, newValue);
                }
            }

            userDetail.UserDetailID = existingUserDetail.UserDetailID;

            _context.Entry(existingUserDetail).State = EntityState.Modified;
        }
        else
        {
            _context.UserDetails.Add(userDetail);
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }


        var city = new CityModel
        {
            CityName = HttpContext.Session.GetString("City"),
        };

        var existingCity = await _context.City.FirstOrDefaultAsync(c => c.CityName == city.CityName);

        if (existingCity == null)
        {
            _context.City.Add(city);
            await _context.SaveChangesAsync();
        }
        else
        {
            city.CityID = existingCity.CityID;
        }

        var province = new ProvinceModel
        {
            ProvinceName = HttpContext.Session.GetString("Province"),
        };

        var existingProvince = await _context.Province.FirstOrDefaultAsync(p => p.ProvinceName == province.ProvinceName);

        if (existingProvince == null)
        {
            _context.Province.Add(province);
            await _context.SaveChangesAsync();
        }
        else
        {
            province.ProvinceID = existingProvince.ProvinceID;
        }

        var country = new CountryModel { CountryName = HttpContext.Session.GetString("Country") };

        var existingCountry = await _context.Country.FirstOrDefaultAsync(c => c.CountryName == country.CountryName);
        if (existingCountry == null)
        {
            _context.Country.Add(country);
            await _context.SaveChangesAsync();
        }
        else
        {
            country.CountryID = existingCountry.CountryID;
        }

        var address = new AddressModel
        {
            UserDetailID = userDetail.UserDetailID,
            CityID = city.CityID,
            ProvinceID = province.ProvinceID,
            CountryID = country.CountryID,
            DetailAddress = HttpContext.Session.GetString("Address"),
        };

        var existingAddress = await _context.Address.FirstOrDefaultAsync(a => a.UserDetailID == userDetail.UserDetailID);

        if (existingAddress != null)
        {
            foreach (var prop in typeof(AddressModel).GetProperties())
            {
                if (prop.Name == nameof(AddressModel.AddressID) || prop.Name == nameof(AddressModel.UserDetailID) || prop.Name == nameof(AddressModel.CreatedAt))
                    continue;

                if (prop.DeclaringType != typeof(AddressModel)) continue;

                var newValue = prop.GetValue(address);
                var oldValue = prop.GetValue(existingAddress);

                if (!object.Equals(newValue, oldValue))
                {
                    prop.SetValue(existingAddress, newValue);
                }
            }

            _context.Entry(existingAddress).State = EntityState.Modified;
        }
        else
        {
            _context.Address.Add(address);
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }


        byte[] fileScanIDBytes = HttpContext.Session.Get("ScanIDFileBytes");
        string fileScanIDName = HttpContext.Session.GetString("ScanIDFileName");
        string saveScanID = SaveImage(fileScanIDBytes, fileScanIDName);
        if (saveScanID != "")
        {
            return BadRequest(saveScanID);
        }

        await UpdateOrInsertFileAsync(user.UserID, fileScanIDName, "Salary Slip");

        var card = new CardsModel()
        {
            CardName = HttpContext.Session.GetString("CardName"),
            CardNumber = GenerateUniqueCardNumber(),
            PinNumber = GeneratePinNumber(),
            UserID = user.UserID,
            CardType = HttpContext.Session.GetString("CardType"),
            CardTier = HttpContext.Session.GetString("CardTier"),
            CVV = new Random().Next(100, 1000),
        };

        _context.Cards.Add(card);
        await _context.SaveChangesAsync();

        double.TryParse(HttpContext.Session.GetString("AnnualFee"), out double annualFee);
        double.TryParse(HttpContext.Session.GetString("CreditLimit"), out double creditLimit);
        int.TryParse(HttpContext.Session.GetString("InterestRate"), out int interestRate);

        var cardDetail = new CardDetailsModel
        {
            CardID = card.CardID,
            AnnualFee = annualFee,
            CreditLimit = creditLimit,
            InterestRate = interestRate,
        };
        _context.CardDetails.Add(cardDetail);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Apply Credit Card Successfully!" });
    }

    private async Task UpdateOrInsertFileAsync(int userId, string fileName, string fileType)
    {
        var existingFile = await _context.Files.FirstOrDefaultAsync(f => f.UserID == userId && f.FileType == fileType);
        string newFilePath = "/uploads/" + fileName;

        if (existingFile != null)
        {
            if (!string.Equals(existingFile.FileName, fileName, StringComparison.OrdinalIgnoreCase))
            {
                string wwwrootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot");

                if (!Directory.Exists(wwwrootPath))
                {
                    Directory.CreateDirectory(wwwrootPath);
                }

                string sanitizedFilePath = existingFile.FilePath.TrimStart('/', '\\');
                string fullPath = Path.Combine(wwwrootPath, sanitizedFilePath);

                DeleteFileIfExists(fullPath);

                existingFile.FileName = fileName;
                existingFile.FilePath = newFilePath;
            }
        }
        else
        {
            var newFile = new FilesModel
            {
                UserID = userId,
                FileName = fileName,
                FileType = fileType,
                FilePath = newFilePath
            };

            _context.Files.Add(newFile);
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    private void DeleteFileIfExists(string filePath)
    {
        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
        }
    }

    private string GenerateUniqueCardNumber()
    {
        Random random = new Random();
        string cardNumber;
        bool isUnique = false;

        do
        {
            cardNumber = string.Concat(Enumerable.Range(0, 16).Select(_ => random.Next(0, 10).ToString()));

            if (!_context.Cards.Any(c => c.CardNumber == cardNumber))
            {
                isUnique = true;
            }

        } while (!isUnique);

        return cardNumber;
    }

    private static string GeneratePinNumber()
    {
        Random random = new Random();

        var pinNumber = string.Concat(
            Enumerable.Range(0, 6).Select(_ => random.Next(0, 10).ToString())
        );

        return pinNumber;
    }
    public string SaveImage(byte[] fileBytes, string fileName)
    {
        if (fileBytes == null || string.IsNullOrEmpty(fileName))
        {
            return "Tidak ada file di Session.";
        }

        string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }

        string filePath = Path.Combine(uploadPath, fileName);
        System.IO.File.WriteAllBytes(filePath, fileBytes);

        return "";
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        );
    }
}

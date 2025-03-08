// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    var cardData = {
        "AbcBankExpensePlatinum": {
            cardType: "Mastercard & Visa",
            cardTier: "Platinum",
            cardColor: "rgba(45, 44, 44, 0.564)",
            cardLogo: "/images/visa_mastercard.png",
            creditLimit: 1000000,
            annualFee: 1000,
            interestRate: "10%",
            interestRateValue: 10,
        },
        "AbcBankShopeeGold": {
            cardType: "Mastercard",
            cardTier: "Gold",
            cardColor: "rgba(222, 208, 2, 0.564)",
            cardLogo: "/images/mastercard.png",
            creditLimit: 50000,
            annualFee: 250,
            interestRate: "13%",
            interestRateValue: 13,
        },
        "AbcBankSyariahStandard": {
            cardType: "Visa",
            cardTier: "Standard",
            cardColor: "rgba(2, 145, 222, 0.564)",
            cardLogo: "/images/visa.png",
            creditLimit: 20000,
            annualFee: 100,
            interestRate: "15%",
            interestRateValue: 15,
        }
    };

    var cardNameText = $("#CardName").text();
    if (cardData.hasOwnProperty(cardNameText)) {
        var details = cardData[cardNameText];

        $(".cc-card").css("background-color", details.cardColor);
        $("#CardLogo").attr("src", details.cardLogo);
    }

    // Event listener untuk perubahan di CardName
    $("#CardName").change(function () {
        var cardName = $(this).val();

        // Cek apakah cardName ada dalam data
        if (cardData.hasOwnProperty(cardName)) {
            var details = cardData[cardName];

            $("#Description").val(details.description);
            $("#CardType").val(details.cardType);
            $("#CardTier").val(details.cardTier);
            $(".cc-card").css("background-color", details.cardColor);
            $("#CardLogo").attr("src", details.cardLogo);
            $("#CreditLimit").val(details.creditLimit);
            $("#AnnualFee").val(details.annualFee);
            $("#InterestRate").val(details.interestRate);
            $("#InterestRateValue").val(details.interestRateValue);
        } else {
            $("#Description").val("");
            $("#CardType").val("");
            $("#CardTier").val("");
            $("#CreditLimit").val("0");
            $("#AnnualFee").val("0");
            $("#InterestRate").val("0%");
            $("#InterestRateValue").val(0);
        }
    });
});
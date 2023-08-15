using Homework_1.Properties;
using Microsoft.AspNetCore.Authentication;
using System.Globalization;
using System.Runtime.Serialization;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/", () => "ƒомашн€€ работа 1\n\n/customs_duty?price=[цена_посылки]f&weight=[вес_посылки]f - стоимость налоговой пошлины;\n/current_date_time?lang=[€з] - узнать сегодн€шнюю дату в подробном формате");
app.MapGet("/customs_duty", (float? price, float? weight) => GetDuty(price, weight));
app.MapGet("/current_date_time", (string lang) => GetCurrentDate(lang));
app.Run();
string GetDuty(float? price, float? weight)
{
    try
    {
        return $"Ќалогова€ пошлина\nЌалогова€ пошлина на вашу посылку составл€ет {(price = price < 200 && weight < 30 ? 0f : price / 100 * 15)} евро.";
    }
    catch(Exception ex)
    {
        return ex.Message;
    }
}
string GetCurrentDate(string lang)
{
    //string result = String.Empty;
    //try
    //{
    //    Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(lang);
    //    result += DateTime.Now.Day + " ";
    //    switch (DateTime.Now.Month)
    //    {
    //        case 1:
    //            result += strings.january;
    //            break;
    //        case 2:
    //            result += strings.february;
    //            break;
    //        case 3:
    //            result += strings.march;
    //            break;
    //        case 4:
    //            result += strings.april;
    //            break;
    //        case 5:
    //            result += strings.may;
    //            break;
    //        case 6:
    //            result += strings.june;
    //            break;
    //        case 7:
    //            result += strings.july;
    //            break;
    //        case 8:
    //            result += strings.august;
    //            break;
    //        case 9:
    //            result += strings.september;
    //            break;
    //        case 10:
    //            result += strings.october;
    //            break;
    //        case 11:
    //            result += strings.november;
    //            break;
    //        case 12:
    //            result += strings.december;
    //            break;
    //    }
    //    result += " " + DateTime.Now.Year + ", ";
    //    switch (DateTime.Now.DayOfWeek)
    //    {
    //        case DayOfWeek.Monday:
    //            result += strings.monday;
    //            break;
    //        case DayOfWeek.Tuesday:
    //            result += strings.tuesday;
    //            break;
    //        case DayOfWeek.Wednesday:
    //            result += strings.wednesday;
    //            break;
    //        case DayOfWeek.Thursday:
    //            result += strings.thursday;
    //            break;
    //        case DayOfWeek.Friday:
    //            result += strings.friday;
    //            break;
    //        case DayOfWeek.Saturday:
    //            result += strings.saturday;
    //            break;
    //        case DayOfWeek.Sunday:
    //            result += strings.sunday;
    //            break;
    //    }
    //    result += ", " + DateTime.Now.ToShortTimeString();
    //}
    //catch (CultureNotFoundException ex)
    //{
    //    result = ex.Message;
    //}
    //return result;
    var culture = CultureInfo.GetCultureInfo(lang);
    var dateTimeFormatInfo = culture.DateTimeFormat;


    return DateTime.Now.ToString(dateTimeFormatInfo.FullDateTimePattern);

}

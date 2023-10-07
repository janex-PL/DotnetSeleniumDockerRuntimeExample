using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

var chromeOptions = new ChromeOptions();
chromeOptions.AddArguments("--headless","--disable-gpu",
    "--no-sandbox", "--disable-dev-shm-usage"); // remember about the necessary arguments when running inside container! 

while (true)
{
    Console.Write("Provide a version of Chrome browser you want to launch (type 'q' to quit):");
    var input = Console.ReadLine();
    
    if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase))
        break;
    
    chromeOptions.BrowserVersion = input;
    var driver = new ChromeDriver(chromeOptions);
    try
    {
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        driver.Navigate().GoToUrl("https://www.whatsmybrowser.org/");
        Console.WriteLine(driver.FindElement(By.TagName("h2")).Text);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
    finally
    {
        driver.Quit();
    }
}
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

ChromeOptions options = new ChromeOptions();
options.AddArguments("--disable-logging", "--disable-gpu", "--no-sandbox", "--disable-dev-shm-usage"); // "--headless", 
options.AddArguments("user-data-dir=" + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Google\Chrome\User Data\"));
IWebDriver driver = new ChromeDriver(@"C:\Program Files\Google\Chrome\Application\", options);
driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
driver.Navigate().GoToUrl("https://www.youtube.com/");


foreach (var line in File.ReadAllLines("songlist.txt")) {
    string artist = line.Split(" - ")[0];
    string song = line.Split('^')[1];
    IWebElement search = driver.FindElement(By.XPath("/html/body/ytd-app/div[1]/div/ytd-masthead/div[3]/div[2]/ytd-searchbox/form/div[1]/div[1]/input"));
    search.Clear();
    search.SendKeys($"{song} {artist} lyrics");
    search.Submit();
    // Click first result
    driver.FindElements(By.XPath("/html/body/ytd-app/div[1]/ytd-page-manager/ytd-search/div[1]/ytd-two-column-search-results-renderer/div/ytd-section-list-renderer/div[2]/ytd-item-section-renderer/div[3]/ytd-video-renderer[1]"))[0].Click();
    // Hit save button
    driver.FindElement(By.XPath("/html/body/ytd-app/div[1]/ytd-page-manager/ytd-watch-flexy/div[5]/div[1]/div/ytd-watch-metadata/div/div[2]/div[2]/div/div/ytd-menu-renderer/div[1]/ytd-button-renderer[3]")).Click();
    // Select playlist to add to
    driver.FindElement(By.XPath("/html/body/ytd-app/ytd-popup-container/tp-yt-paper-dialog/ytd-add-to-playlist-renderer/div[2]/ytd-playlist-add-to-option-renderer[2]")).Click();
    // Close out of add to playlist popup
    driver.FindElement(By.XPath("/html/body/ytd-app/ytd-popup-container/tp-yt-paper-dialog/ytd-add-to-playlist-renderer/div[1]/yt-icon-button/button")).Click();
}
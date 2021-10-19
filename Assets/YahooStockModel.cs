// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
// class generated with:  https://json2csharp.com/
using System.Collections.Generic;
public class Criterion
{
    public string field { get; set; }
    public List<string> operators { get; set; }
    public List<double> values { get; set; }
    public List<int> labelsSelected { get; set; }
}

public class CriteriaMeta
{
    public int size { get; set; }
    public int offset { get; set; }
    public string sortField { get; set; }
    public string sortType { get; set; }
    public string quoteType { get; set; }
    public string topOperator { get; set; }
    public List<Criterion> criteria { get; set; }
}

public class TwoHundredDayAverageChangePercent
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class FiftyTwoWeekLowChangePercent
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class RegularMarketDayRange
{
    public string raw { get; set; }
    public string fmt { get; set; }
}

public class RegularMarketDayHigh
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class TwoHundredDayAverageChange
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class AskSize
{
    public int raw { get; set; }
    public string fmt { get; set; }
    public string longFmt { get; set; }
}

public class TwoHundredDayAverage
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class BookValue
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class MarketCap
{
    public object raw { get; set; }
    public string fmt { get; set; }
    public string longFmt { get; set; }
}

public class FiftyTwoWeekHighChange
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class FiftyTwoWeekRange
{
    public string raw { get; set; }
    public string fmt { get; set; }
}

public class FiftyDayAverageChange
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class AverageDailyVolume3Month
{
    public int raw { get; set; }
    public string fmt { get; set; }
    public string longFmt { get; set; }
}

public class FiftyTwoWeekLow
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class RegularMarketVolume
{
    public int raw { get; set; }
    public string fmt { get; set; }
    public string longFmt { get; set; }
}

public class RegularMarketDayLow
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class FiftyDayAverageChangePercent
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class RegularMarketOpen
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class RegularMarketTime
{
    public int raw { get; set; }
    public string fmt { get; set; }
}

public class RegularMarketChangePercent
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class AverageDailyVolume10Day
{
    public int raw { get; set; }
    public string fmt { get; set; }
    public string longFmt { get; set; }
}

public class FiftyTwoWeekLowChange
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class FiftyTwoWeekHighChangePercent
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class SharesOutstanding
{
    public long raw { get; set; }
    public string fmt { get; set; }
    public string longFmt { get; set; }
}

public class RegularMarketPreviousClose
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class FiftyTwoWeekHigh
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class BidSize
{
    public int raw { get; set; }
    public string fmt { get; set; }
    public string longFmt { get; set; }
}

public class RegularMarketChange
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class FiftyDayAverage
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class RegularMarketPrice
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class Ask
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class EpsTrailingTwelveMonths
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class Bid
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class PriceToBook
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class DividendDate
{
    public int raw { get; set; }
    public string fmt { get; set; }
    public string longFmt { get; set; }
}

public class EpsForward
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class TrailingPE
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class PriceEpsCurrentYear
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class EpsCurrentYear
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class ForwardPE
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class EarningsTimestampEnd
{
    public int raw { get; set; }
    public string fmt { get; set; }
    public string longFmt { get; set; }
}

public class EarningsTimestampStart
{
    public int raw { get; set; }
    public string fmt { get; set; }
    public string longFmt { get; set; }
}

public class EarningsTimestamp
{
    public int raw { get; set; }
    public string fmt { get; set; }
    public string longFmt { get; set; }
}

public class TrailingAnnualDividendRate
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class PostMarketPrice
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class TrailingAnnualDividendYield
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class PostMarketTime
{
    public int raw { get; set; }
    public string fmt { get; set; }
}

public class PostMarketChangePercent
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class PostMarketChange
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class Quote
{
    public string symbol { get; set; }
    public TwoHundredDayAverageChangePercent twoHundredDayAverageChangePercent { get; set; }
    public FiftyTwoWeekLowChangePercent fiftyTwoWeekLowChangePercent { get; set; }
    public string language { get; set; }
    public RegularMarketDayRange regularMarketDayRange { get; set; }
    public RegularMarketDayHigh regularMarketDayHigh { get; set; }
    public TwoHundredDayAverageChange twoHundredDayAverageChange { get; set; }
    public AskSize askSize { get; set; }
    public TwoHundredDayAverage twoHundredDayAverage { get; set; }
    public BookValue bookValue { get; set; }
    public MarketCap marketCap { get; set; }
    public FiftyTwoWeekHighChange fiftyTwoWeekHighChange { get; set; }
    public FiftyTwoWeekRange fiftyTwoWeekRange { get; set; }
    public FiftyDayAverageChange fiftyDayAverageChange { get; set; }
    public int exchangeDataDelayedBy { get; set; }
    public object firstTradeDateMilliseconds { get; set; }
    public AverageDailyVolume3Month averageDailyVolume3Month { get; set; }
    public FiftyTwoWeekLow fiftyTwoWeekLow { get; set; }
    public RegularMarketVolume regularMarketVolume { get; set; }
    public string market { get; set; }
    public string quoteSourceName { get; set; }
    public string messageBoardId { get; set; }
    public int priceHint { get; set; }
    public RegularMarketDayLow regularMarketDayLow { get; set; }
    public int sourceInterval { get; set; }
    public string exchange { get; set; }
    public string shortName { get; set; }
    public string region { get; set; }
    public FiftyDayAverageChangePercent fiftyDayAverageChangePercent { get; set; }
    public string fullExchangeName { get; set; }
    public string financialCurrency { get; set; }
    public string displayName { get; set; }
    public int gmtOffSetMilliseconds { get; set; }
    public RegularMarketOpen regularMarketOpen { get; set; }
    public RegularMarketTime regularMarketTime { get; set; }
    public RegularMarketChangePercent regularMarketChangePercent { get; set; }
    public string quoteType { get; set; }
    public AverageDailyVolume10Day averageDailyVolume10Day { get; set; }
    public FiftyTwoWeekLowChange fiftyTwoWeekLowChange { get; set; }
    public FiftyTwoWeekHighChangePercent fiftyTwoWeekHighChangePercent { get; set; }
    public bool tradeable { get; set; }
    public string currency { get; set; }
    public SharesOutstanding sharesOutstanding { get; set; }
    public RegularMarketPreviousClose regularMarketPreviousClose { get; set; }
    public FiftyTwoWeekHigh fiftyTwoWeekHigh { get; set; }
    public string exchangeTimezoneName { get; set; }
    public BidSize bidSize { get; set; }
    public RegularMarketChange regularMarketChange { get; set; }
    public FiftyDayAverage fiftyDayAverage { get; set; }
    public string exchangeTimezoneShortName { get; set; }
    public string marketState { get; set; }
    public RegularMarketPrice regularMarketPrice { get; set; }
    public Ask ask { get; set; }
    public EpsTrailingTwelveMonths epsTrailingTwelveMonths { get; set; }
    public Bid bid { get; set; }
    public bool triggerable { get; set; }
    public PriceToBook priceToBook { get; set; }
    public string longName { get; set; }
    public DividendDate dividendDate { get; set; }
    public EpsForward epsForward { get; set; }
    public TrailingPE trailingPE { get; set; }
    public PriceEpsCurrentYear priceEpsCurrentYear { get; set; }
    public EpsCurrentYear epsCurrentYear { get; set; }
    public ForwardPE forwardPE { get; set; }
    public string averageAnalystRating { get; set; }
    public EarningsTimestampEnd earningsTimestampEnd { get; set; }
    public EarningsTimestampStart earningsTimestampStart { get; set; }
    public EarningsTimestamp earningsTimestamp { get; set; }
    public string prevName { get; set; }
    public string nameChangeDate { get; set; }
    public TrailingAnnualDividendRate trailingAnnualDividendRate { get; set; }
    public PostMarketPrice postMarketPrice { get; set; }
    public TrailingAnnualDividendYield trailingAnnualDividendYield { get; set; }
    public PostMarketTime postMarketTime { get; set; }
    public PostMarketChangePercent postMarketChangePercent { get; set; }
    public PostMarketChange postMarketChange { get; set; }
    public string ipoExpectedDate { get; set; }
}

public class Result
{
    public string id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public string canonicalName { get; set; }
    public CriteriaMeta criteriaMeta { get; set; }
    public string rawCriteria { get; set; }
    public int start { get; set; }
    public int count { get; set; }
    public int total { get; set; }
    public List<Quote> quotes { get; set; }
    public bool useRecords { get; set; }
    public bool predefinedScr { get; set; }
    public int versionId { get; set; }
    public long creationDate { get; set; }
    public long lastUpdated { get; set; }
    public bool isPremium { get; set; }
}

public class Finance
{
    public List<Result> result { get; set; }
    public object error { get; set; }
}

public class YahooStockModel
{
    public Finance finance { get; set; }
}

using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http;

public static class QuoteEndpoint
{
    public static void RegisterQuoteEndpoint(this WebApplication app)
    {
        // 静的リスト作成
        List<Quote> quotes = new List<Quote>
        {
            new Quote{text = "夢はでっかく根はふかく", author = "相田みつを"},
            new Quote{text = "夢見ることができれば、それは実現できる", author = "ウォルト・ディズニー"},
            new Quote{text = "それでも地球は動いている", author = "ガリレオ・ガリレイ"},
            new Quote{text = "もし、黄色と橙色がなければ、青色もない", author = "ゴッホ"},
            new Quote{text = "もう一押しこそ慎重になれ", author = "武田信玄"}
        };

        // パスのグルーピング
        var path = app.MapGroup("/api");
        path.MapGet("/quotes/{author}", GetQuoteByAuthor);
        
        /// <summary>
        /// 特定の著者の名言を返す
        /// </summary>
        /// <param name="author">著者</param>
        async Task<IResult> GetQuoteByAuthor(string author)
        {
            var quote = quotes.Where(q => q.author == author);
            return Results.Ok(quote);
        }
    } 
}






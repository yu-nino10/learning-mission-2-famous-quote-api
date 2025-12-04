// ### 課題2：「名言ジェネレーターAPI」
// **期間：1-2週間**
// **難易度：★★☆☆☆**

// **習得する技術：**
// - LINQ（Where, Select, OrderBy）
// - List操作
// - ランダムな要素の取得
// - クエリパラメータの扱い
// - POSTリクエストの処理

// **作るもの：**
// 名言をランダムに返したり、追加・検索できるAPI

// **必須要件：**
// GET /api/quotes/random
// → ランダムに1つの名言を返す

// GET /api/quotes?author=名前
// → 特定の著者の名言を返す

// GET /api/quotes?search=キーワード
// → キーワードを含む名言を返す

// POST /api/quotes
// → 新しい名言を追加（メモリ内リストに保存）

// Body: { "text": "名言", "author": "著者" }

// **ボーナス要件：**
// GET /api/quotes/authors
// → 登録されている著者一覧を返す（LINQ の Distinct 使用）

// GET /api/quotes/stats
// → 統計情報を返す（総数、著者数など）

// **実装の流れ：**
// 1. Quote モデルクラス作成
// 2. 静的リスト（List<Quote>）で初期データ準備
// 3. LINQ を使った検索・フィルタリング実装
// 4. POST エンドポイント実装
// 5. クエリパラメータの処理

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
        path.MapGet("/quotes/random", GetQuoteRandom);
        
        /// <summary>
        /// 特定の著者の名言を返す
        /// </summary>
        /// <param name="author">著者</param>
        IResult GetQuoteByAuthor(string author)
        {
            var quote = quotes.Where(q => q.author == author);
            return TypedResults.Ok(quote);
        }

        /// <summary>
        /// ランダムに1つの名言を返す
        /// </summary>
        IResult GetQuoteRandom()
        {
            var quote = quotes.OrderBy(q => Guid.NewGuid()).Take(1);
            return TypedResults.Ok(quote);
        }
    } 
}






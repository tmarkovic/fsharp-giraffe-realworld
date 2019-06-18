namespace Conduit.Infrastructure

open Microsoft.Extensions.DependencyInjection
open System.Collections
open Conduit.Domain.Articles
open Conduit.Domain.Common

module ArticlesInMemory =
    let find (inMemory : Hashtable) (criteria : ArticleFilter)  =
        match criteria with
        | All ->
            inMemory.Values
            |> Seq.cast
            |> Array.ofSeq
        | Slug s ->
            inMemory.Values
            |> Seq.cast
            |> Seq.filter (fun (a:Article) -> a.Slug.Equals(s))
            |> Array.ofSeq

    let save (inMemory : Hashtable) (article : Article) : Article =
        inMemory.Add(article.Slug, article) |> ignore
        article

    type IServiceCollection with
        member this.AddArticlesInMemory(inMemory : Hashtable) =
            this.AddSingleton<ArticleFind>(find inMemory) |> ignore
            this.AddSingleton<ArticleSave>(save inMemory) |> ignore

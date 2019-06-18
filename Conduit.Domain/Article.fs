namespace Conduit.Domain.Articles
open System
open Conduit.Domain.Common

  type Article = {
    Slug: string
    Title: String50
    Description: string
    Body: string
    TagList: string array
    CreatedAt: DateTime
    UpdatedAt: DateTime option
    Favorited: bool option
    FavoritesCount: int
    }

type ArticleFilter =
    | All
    | Slug of string

type ArticleFind = ArticleFilter -> Article[]

type ArticleSave = Article -> Article

module testing =
    let someArticle article=
      {article with Title = (match createString50 "asdasdasd" with | None -> String50 "" | Some x -> x)}
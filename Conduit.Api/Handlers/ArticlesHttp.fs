namespace Articles.Http

open Giraffe
open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks.V2
open Conduit.Domain.Articles
open Conduit.Domain.Common
open System

module ArticlesHttp =
    let handlers : HttpFunc -> HttpContext -> HttpFuncResult =
        choose
            [ POST >=> route "/articles" >=> fun next context ->
                  task {
                      let save = context.GetService<ArticleSave>()
                      let! article = context.BindJsonAsync<Article>()
                      let article = { article with Slug = slugify "" ; CreatedAt = DateTime.Now}
                      return! json (save article) next context
                  }
              GET >=> route "/articles" >=> fun next context ->
                  let find = context.GetService<ArticleFind>()
                  let articles = find ArticleFilter.All
                  json articles next context
              GET >=> routef "/articles/%s" (fun slug next context ->
                          let find = context.GetService<ArticleFind>()
                          let article = find (ArticleFilter.Slug slug)
                          match Array.tryHead article with
                            | Some head -> json head next context
                            | _ -> RequestErrors.notFound (text "") next context
                          )

              PUT
              >=> routef "/articles/%s"
                      (fun slug next context ->
                      text ("Update " + slug) next context)

              DELETE
              >=> routef "/articles/%s"
                      (fun slug next context ->
                      text ("Delete " + slug) next context) ]

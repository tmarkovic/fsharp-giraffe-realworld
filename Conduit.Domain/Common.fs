namespace Conduit.Domain


open System.Text.RegularExpressions
open System

module Common =
    let clean (s : string) : string = Regex.Replace(s, "[^a-zA-Z0-9\s]", "")
    let kebab (s : string) : string = Regex.Replace(s, "\s+", "-")
    let toLower (s : string) : string = String.map Char.ToLower s

    let slugify (s : string) : string =
        s
        |> clean
        |> kebab
        |> toLower

    type String50 = String50 of string

    let createString50 (s:string) =
      if s.Length <= 50
        then Some (String50 s)
        else None

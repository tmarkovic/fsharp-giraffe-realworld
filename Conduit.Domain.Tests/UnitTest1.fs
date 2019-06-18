namespace Tests

open NUnit.Framework
open Conduit.Domain.Common

[<TestClass>]
type TestClass() =

    [<SetUp>]
    member this.Setup() = ()

    [<Test>]
    member this.ToLower() =
        let expected = "asd"
        let actual = toLower <| slugify "ASD"
        Assert.That(actual, Is.EqualTo(expected))
    [<Test>]
    member this.Trim() =
        let expected = "asd"
        let actual = toLower <| slugify " asd "
        Assert.That(actual, Is.EqualTo(expected))
    [<Test>]
    member this.Clean() =
        let expected = "asd"
        let actual = clean "©@£$∞asd"
        Assert.That(actual, Is.EqualTo(expected))

    [<Test>]
    member this.WithSpaces() =
        let expected = "asd-asd-asd"
        let actual = slugify "ASD ASD ASD"
        Assert.That(actual, Is.EqualTo(expected))

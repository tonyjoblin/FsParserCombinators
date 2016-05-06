module MapPUnitTests

open NUnit.Framework
open parser

[<TestFixture>]
type mapPUnitTestFixture() = 

    member self.parseA = pchar 'A'

    [<Test>]
    member self.TestErrorResultIsUnchanged() =
        let input = "B"
        let transform result = "foobar"
        let parser = mapP transform self.parseA

        let result = run parser input

        match result with 
        | Success _ -> Assert.Fail()
        | Failure "Expecting 'A'. Got 'B'" -> () // expected
        | Failure _ -> Assert.Fail()

    [<Test>]
    member self.TestTransformIsAppliedOnSuccess() =
        let input = "A"
        let transform result = "foobar"
        let parser = mapP transform self.parseA

        let result = run parser input

        match result with 
        | Success ("foobar", "") -> () // expected
        | _ -> Assert.Fail()

    [<Test>]
    member self.TestInfixOperator() =
        let input = "A"
        let transform result = "foobar"
        let parser = transform <!> self.parseA

        let result = run parser input

        match result with 
        | Success ("foobar", "") -> () // expected
        | _ -> Assert.Fail()

    [<Test>]
    member self.TestCompositionOperator() =
        let input = "A"
        let transform result = "foobar"
        let parser = self.parseA |>> transform

        let result = run parser input

        match result with 
        | Success ("foobar", "") -> () // expected
        | _ -> Assert.Fail()

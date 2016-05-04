module parser
open System

type Result<'a> = 
    | Success of 'a
    | Failure of string

type Parser<'T> = Parser of (string -> Result<'T * string>)

let pchar charToMatch =

    let inner str =
        if String.IsNullOrEmpty(str) then
            Failure "No more input"
        else if str.[0] = charToMatch then
            let msg = sprintf "Found %c" charToMatch
            let remaining = str.[1..]
            Success (charToMatch, remaining)
        else
            let first = str.[0]
            let msg = sprintf "Expecting '%c'. Got '%c'" charToMatch first
            Failure msg

    Parser inner

let parseA = pchar 'A'

let parseB = pchar 'B'

let run parser input =
    let (Parser fn) = parser
    fn input

let andThen (parser1: Parser<'a>) (parser2: Parser<'a>) =

    let innerFn input =
        let result1 = run parser1 input

        match result1 with
        | Failure err -> 
            Failure err
        | Success (value1, remaining) ->
            let result2 = run parser2 remaining
            match result2 with
            | Failure err ->
                Failure err
            | Success (value2, remaining2) ->
                Success((value1, value2), remaining2)

    Parser innerFn

let (.>>.) = andThen

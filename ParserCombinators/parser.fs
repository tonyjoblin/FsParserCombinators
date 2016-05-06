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

let run parser input =
    let (Parser fn) = parser
    fn input

let andThen parser1 parser2 =

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


let orElse parser1 parser2 =
    
    let innerFn input = 
        let result1 = run parser1 input
        match result1 with
        | Failure err ->
            run parser2 input
        | Success result ->
            result1

    Parser innerFn

let (<|>) = orElse

let choice listOfParsers =
    List.reduce (<|>) listOfParsers

let anyOf listOfChars =
    listOfChars
    |> List.map pchar
    |> choice

let parseLowerCase =
    anyOf ['a'..'z']

let parseDigit =
    anyOf ['0'..'9']

let mapP f parser =
    
    let innerFn input =
        let result = run parser input
        match result with
        | Success (value, remaining) ->
            let newValue = f value
            Success (newValue, remaining)
        | Failure err -> 
            Failure err
    Parser innerFn

let (<!>) = mapP

let (|>>) x f = mapP f x

let parseThreeDigitsAsStr =
    let tupleParser = parseDigit .>>. parseDigit .>>. parseDigit
    let transformTuple ((c1, c2), c3) =
        String [|c1; c2; c3|]
    mapP transformTuple tupleParser


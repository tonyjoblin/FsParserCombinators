module parser
open System

type Result<'a> = 
    | Success of 'a
    | Failure of string

let A_Parser str =
    if String.IsNullOrEmpty(str) then
        (false, "")
    else if str.[0] = 'A' then
        let remaining = str.[1..]
        (true, remaining)
    else
        (false, str)

let pchar charToMatch str =
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



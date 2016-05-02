module parser
open System

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
        let msg = "No more input"
        (msg, "")
    else if str.[0] = charToMatch then
        let msg = sprintf "Found %c" charToMatch
        let remaining = str.[1..]
        (msg, remaining)
    else
        let first = str.[0]
        let msg = sprintf "Expecting '%c'. Got '%c'" charToMatch first
        (msg, str) 



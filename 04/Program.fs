open Extensions
open FSharp.Text.RegexExtensions
open FSharp.Text.RegexProvider
open InputUtils

type AssignmentsRegex = Regex< @"(?<from1>\d+)-(?<to1>\d+),(?<from2>\d+)-(?<to2>\d+)">

let assignmentsMatcher = AssignmentsRegex()

type Assignment = (int*int)*(int*int)

let part1 (assignments: Assignment seq) =
    let oneFullyContainsOther (((f1, t1), (f2, t2)): Assignment) =
        (f1 >= f2 && t1 <= t2) // 1 contained in 2
        || (f2 >= f1 && t2 <= t1) // 2 contained in 1

    assignments
    |> Seq.filter oneFullyContainsOther
    |> Seq.length

let part2 (assignments: Assignment seq) =
    let doOverlap (((f1, t1), (f2, t2)): Assignment) =
        (f1 >= f2 && f1 <= t2) // f1 is in f2-t2
        || (t1 >= f2 && t1 <= t2) // t1 is in f2-t2
        || (f2 >= f1 && f2 <= t1) // f2 is in f1-t1
        || (t2 >= f1 && t2 <= t1) // t2 is in f1-t1

    assignments
    |> Seq.filter doOverlap
    |> Seq.length


[<EntryPoint>]
let main argv =
    let input =
        System.IO.File.ReadLines (getFileName argv)
        |> Seq.map (assignmentsMatcher.TypedMatch)
        |> Seq.map (fun m -> ((m.from1.AsInt, m.to1.AsInt), (m.from2.AsInt, m.to2.AsInt)))
        |> Seq.cache

    part1 input
    |> printfn "%d"

    part2 input
    |> printfn "%d"

    0


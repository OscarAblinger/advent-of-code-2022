open Extensions
open Extensions.Map
open Extensions.Seq
open FSharp.Text.RegexExtensions
open FSharp.Text.RegexProvider
open InputUtils

type CratesRegex = Regex< @"\[(?<crate>.)\] ?">
let cratesParser = CratesRegex()

type InstructionsRegex = Regex< @"move (?<amount>\d+) from (?<from>\d) to (?<to>\d)">
let instructionsParser = InstructionsRegex()

[<Struct>]
type Instruction = {
    Amount: int
    From: int
    To: int
}

let parseCrates (crates: string seq) =
    let foldLine (map: Map<int, char list>) (matches: (int*char) seq) =
        let prepend (ch: char) (curr: char list option) =
            match curr with
            | Some v -> Some (ch::v)
            | None -> Some [ch]

        matches
        |> Seq.fold (fun (m: Map<int,char list>) (idx, ch) -> m.Change (idx, (prepend ch))) map

    crates
    |> Seq.takeWhile (fun str -> not (str.StartsWith " 1 "))
    |> Seq.map cratesParser.TypedMatches
    |> Seq.map (Seq.map (fun m -> (m.Index/4) + 1, m.crate.AsChar))
    |> Seq.rev
    |> Seq.fold foldLine Map.empty

let parseInstructions (instructions: string seq) =
    instructions
    |> Seq.map instructionsParser.TypedMatch
    |> Seq.map (fun m -> { Amount = m.amount.AsInt; From = m.from.AsInt; To = m.``to``.AsInt })

// ========== part 1 ==========
let part1 (crates: Map<int, char list>) (instructions: Instruction seq) =
    let move1 (crates: Map<int, char list>) src dest =
        crates
        |> Map.change dest (fun v ->
            match v with
            | Some chars -> Some ((crates.[src].Head)::chars)
            | None -> Some ([crates.[src].Head])
        )
        |> Map.change src (fun v ->
            match v with
            | Some (_::tail) -> Some tail
            | Some []
            | None -> failwithf "Tried to remove elment from empty list"
        )

    let rec apply (crates: Map<int, char list>) (instruction: Instruction) =
        match instruction with
        | {Amount = 0} -> crates
        | _ -> apply (move1 crates instruction.From instruction.To) {instruction with Amount = instruction.Amount-1}

    instructions
    |> Seq.fold apply crates
    |> mapValues (List.head)
    |> Seq.sortBy (fun entry -> entry.Key)
    |> Seq.map (fun entry -> entry.Value)
    |> System.String.Concat

// ========== part 2 ==========
let part2 (crates: Map<int, char list>) (instructions: Instruction seq) =
    let apply (crates: Map<int, char list>) (instruction: Instruction) =
        crates
        |> Map.change instruction.To (fun value ->
            match value with
            | Some v -> 
                Some ((crates.[instruction.From] |> List.take instruction.Amount) @ crates.[instruction.To])
            | None -> Some (crates.[instruction.From] |> List.take instruction.Amount)
        )
        |> Map.change instruction.From (fun value ->
            match value with
            | Some v -> 
                Some (crates.[instruction.From] |> List.skip instruction.Amount)
            | None -> failwithf "Tried to remove elments from empty list"
        )

    instructions
    |> Seq.fold apply crates
    |> mapValues (List.head)
    |> Seq.sortBy (fun entry -> entry.Key)
    |> Seq.map (fun entry -> entry.Value)
    |> System.String.Concat

[<EntryPoint>]
let main argv =
    let input =
        System.IO.File.ReadLines (getFileName argv)
        |> splitWhen ((=)"")
        |> List.ofSeq

    let [cratesText; instructionsText] = input

    let crates = parseCrates cratesText
    let instructions = parseInstructions instructionsText

    part1 crates instructions
    |> printfn "%A"

    part2 crates instructions
    |> printfn "%A"

    0


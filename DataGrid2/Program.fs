namespace DataGrid2

open System
open Elmish
open Elmish.WPF


module Cell =
    type Model =
        { Id: Guid
          CellName: string }

    let init i j =
        { Id = Guid.NewGuid ()
          CellName = sprintf "CellName %i  %i" i j }

    let bindings() = [
        "CellName" |> Binding.oneWay(fun (_, c) -> c.CellName)
        "SelectedLabel" |> Binding.oneWay (fun (b, _) -> if b then " - Selected" else "")
     ]


module OutterRow =

    type Model =
      { Id: Guid
        OutterRowName: string
        InnerRows: Cell.Model list
        SelectedInnerRow: Guid option }

    type Msg =
         | Select of Guid option

    let init i =
        { Id = Guid.NewGuid ()
          OutterRowName = sprintf "RowName %i" i
          InnerRows =  [0 .. 2] |> List.map (Cell.init i)
          SelectedInnerRow = None }

    let update msg m =
      match msg with
      | Select id -> { m with SelectedInnerRow = id }

    let bindings() : Binding<bool * Model, Msg> list = [
           "OutterRowName" |> Binding.oneWay(fun (b, p) -> p.OutterRowName + (if b then " - Selected" else ""))
           "InnerRows" |> Binding.subModelSeq(
               (fun (_, p) -> p.InnerRows),
               (fun ((b, p), c) -> (b && p.SelectedInnerRow = Some c.Id, c)),
               (fun (_, c) -> c.Id),
               snd,
               Cell.bindings)
           "SelectedInnerRow" |> Binding.subModelSelectedItem("InnerRows", (fun (_, r) -> r.SelectedInnerRow), (fun cId _ -> Select cId))
    ]

    
module App =

   type Model =
      { OutterRows: OutterRow.Model list
        SelectedOutterRow: Guid option }

   let init () =
     {  OutterRows = [0 .. 2] |> List.map OutterRow.init
        SelectedOutterRow = None }

   type Msg =
      | Select of Guid option
      | RowMsg of Guid * OutterRow.Msg

   let update msg m =
      match msg with
      | Select rId -> { m with SelectedOutterRow = rId }
      | RowMsg (rId, msg) ->
          let rows =
            m.OutterRows
            |> List.map (fun r -> if r.Id = rId then OutterRow.update msg r else r)
          { m with OutterRows = rows }

   let bindings () = [
        "OutterRows" |> Binding.subModelSeq(
            (fun m -> m.OutterRows),
            (fun (m, r) -> (m.SelectedOutterRow = Some r.Id, r)),
            (fun (_, r) -> r.Id),
            RowMsg,
            OutterRow.bindings)
        "SelectedOutterRow" |> Binding.subModelSelectedItem("OutterRows", (fun m -> m.SelectedOutterRow), Select)
   ]

   let main window =
      Program.mkSimpleWpf init update bindings
      |> Program.withConsoleTrace
      |> Program.runWindowWithConfig
        { ElmConfig.Default with LogConsole = true; Measure = false }
        window
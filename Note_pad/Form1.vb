Imports System
Imports System.IO
Imports iTextSharp
Imports iTextSharp.text.pdf
Imports iTextSharp.text


Public Class Form1
    Private Sub TimeAndDateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TimeAndDateToolStripMenuItem.Click
        Rtd_bx.Text = System.DateTime.Now.ToString
        'Hello World
    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click

        If Rtd_bx.Modified Then

            Dim ask As MsgBoxResult
            ask = MsgBox("Do you want to save the changes", MsgBoxStyle.YesNoCancel, "New Document")
            If ask = MsgBoxResult.No Then
                Rtd_bx.Clear()
            ElseIf ask = MsgBoxResult.Cancel Then
            ElseIf ask = MsgBoxResult.Yes Then
                SaveFileDialog1.ShowDialog()
                My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, Rtd_bx.Text, False)
                Rtd_bx.Clear()
            End If

        Else
            Rtd_bx.Clear()
        End If
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click

        If Rtd_bx.Modified Then

            Dim ask As MsgBoxResult
            ask = MsgBox("Do you want to save the changes", MsgBoxStyle.YesNoCancel, "Open Document")
            If ask = MsgBoxResult.No Then
                OpenFileDialog1.ShowDialog()
                Rtd_bx.Text = My.Computer.FileSystem.ReadAllText(OpenFileDialog1.FileName)
            ElseIf ask = MsgBoxResult.Cancel Then
            ElseIf ask = MsgBoxResult.Yes Then
                SaveFileDialog1.ShowDialog()
                My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, Rtd_bx.Text, False)
                Rtd_bx.Clear()
            End If
        Else

            OpenFileDialog1.ShowDialog()
            Try
                Rtd_bx.Text = My.Computer.FileSystem.ReadAllText(OpenFileDialog1.FileName)
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click

        SaveFileDialog1.ShowDialog()

        If My.Computer.FileSystem.FileExists(SaveFileDialog1.FileName) Then
            Dim ask As MsgBoxResult
            ask = MsgBox("File already exists, would you like to replace it?", MsgBoxStyle.YesNo, "File Exists")

            If ask = MsgBoxResult.No Then
                SaveFileDialog1.ShowDialog()

            ElseIf ask = MsgBoxResult.Yes Then
                My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, Rtd_bx.Text, False)
            End If

        Else
            Try
                My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, Rtd_bx.Text, False)
            Catch ex As Exception
            End Try
        End If

    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub UndoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UndoToolStripMenuItem.Click
        If Rtd_bx.CanUndo Then
            Rtd_bx.Undo()
        Else
        End If
    End Sub

    Private Sub CutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CutToolStripMenuItem.Click
        My.Computer.Clipboard.Clear()
        If Rtd_bx.SelectionLength > 0 Then
            My.Computer.Clipboard.SetText(Rtd_bx.SelectedText)

        End If
        Rtd_bx.SelectedText = ""
    End Sub

    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        My.Computer.Clipboard.Clear()
        If Rtd_bx.SelectionLength > 0 Then
            My.Computer.Clipboard.SetText(Rtd_bx.SelectedText)

        End If
    End Sub

    Private Sub PasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem.Click
        If My.Computer.Clipboard.ContainsText Then
            Rtd_bx.Paste()
        End If
    End Sub

    Private Sub SelectAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectAllToolStripMenuItem.Click
        Rtd_bx.SelectAll()
    End Sub

    Private Sub FindToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FindToolStripMenuItem.Click
        Dim a As String
        Dim b As String
        a = InputBox("Enter text to be found")
        b = InStr(Rtd_bx.Text, a)
        If b Then
            Rtd_bx.Focus()
            Rtd_bx.SelectionStart = b - 1
            Rtd_bx.SelectionLength = Len(a)
        Else
            MsgBox("Text not found.")
        End If
    End Sub

    Private Sub ReplaceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReplaceToolStripMenuItem.Click
        Dim a, b, c As String
        a = InputBox("Enter Text To Be Replaced")
        c = InputBox("Enter Text To Replace")
        b = InStr(Rtd_bx.Text, a)
        If b Then
            Rtd_bx.Focus()
            Rtd_bx.SelectionStart = b - 1
            Rtd_bx.SelectionLength = Len(a)
            Rtd_bx.SelectedText = c
        Else
            MsgBox("Text not found.")
        End If
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click

        Rtd_bx.Clear()


    End Sub

    Private Sub FontToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FontToolStripMenuItem.Click
        FontDialog1.ShowDialog()
        Rtd_bx.Font = FontDialog1.Font
    End Sub

    Private Sub PrintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintToolStripMenuItem.Click
        PrintDialog1.Document = PrintDocument1
        PrintDialog1.PrinterSettings = PrintDocument1.PrinterSettings
        PrintDialog1.AllowSomePages = True
        If PrintDialog1.ShowDialog = DialogResult.OK Then
            PrintDocument1.PrinterSettings = PrintDialog1.PrinterSettings
            PrintDocument1.Print()
        End If
    End Sub

    Private Sub WordWrapToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WordWrapToolStripMenuItem.Click

        If Rtd_bx.WordWrap = True Then
            Rtd_bx.WordWrap = False
        ElseIf Rtd_bx.WordWrap = False Then
            Rtd_bx.WordWrap = True
        End If
    End Sub

    Private Sub ExportToPdfToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportToPdfToolStripMenuItem.Click
        If Rtd_bx.Text <> vbNullString Then
            SaveFileDialog2.ShowDialog()
            Dim pdfdoc As New Document
            Try
                Dim pdfwrite As PdfWriter = PdfWriter.GetInstance(pdfdoc, New FileStream(SaveFileDialog2.FileName, FileMode.Create))
                MessageBox.Show("PDF Created Successfully.")
            Catch ex As Exception
            End Try
            pdfdoc.Open()
            pdfdoc.Add(New Paragraph(Rtd_bx.Text))
            pdfdoc.Close()

        Else
            MessageBox.Show("Please Write Something")
        End If


    End Sub

    Private Sub Rtd_bx_TextChanged(sender As Object, e As EventArgs) Handles Rtd_bx.TextChanged
        Rtd_bx.WordWrap = False

        If Rtd_bx.Text = vbNullString Then
            UndoToolStripMenuItem.Enabled = False
            CutToolStripMenuItem.Enabled = False
            CopyToolStripMenuItem.Enabled = False

            SelectAllToolStripMenuItem.Enabled = False
            ReplaceToolStripMenuItem.Enabled = False
            FindToolStripMenuItem.Enabled = False
            DeleteToolStripMenuItem.Enabled = False
        ElseIf Rtd_bx.Text <> vbNullString Then
            UndoToolStripMenuItem.Enabled = True
            CutToolStripMenuItem.Enabled = True
            CopyToolStripMenuItem.Enabled = True

            SelectAllToolStripMenuItem.Enabled = True
            ReplaceToolStripMenuItem.Enabled = True
            FindToolStripMenuItem.Enabled = True
            DeleteToolStripMenuItem.Enabled = True
        End If
    End Sub


End Class

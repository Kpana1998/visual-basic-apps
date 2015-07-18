﻿Imports System.IO

Public Class Form1
    ' Class-level variables
    Private strFilename As String = String.Empty    ' Document filename
    Dim blnIsChanged As Boolean = False             ' File change flag

    Sub ClearDocumnet()
        ' Clear the contents of the text box.
        txtDocument.Clear()

        ' Clear the documnet name.
        strFilename = String.Empty

        ' Set is Changed to False.
        blnIsChanged = False
    End Sub

    ' The OpenDocument procedure opens a file and loads it
    ' into the TextBox for editing.

    Sub OpenDocument()
        Dim inputFile As StreamReader   ' Object variable

        If ofdOpenFile.ShowDialog = Windows.Forms.DialogResult.OK Then
            ' Retieve the elected filename.
            strFilename = ofdOpenFile.FileName

            Try
                ' Open the file.
                inputFile = File.OpenText(strFilename)

                ' Read thr file's contents into the TextBox.
                txtDocument.Text = inputFile.ReadToEnd

                ' Close the file.
                inputFile.Close()

                ' Update the isChanged variable.
                blnIsChanged = False
            Catch
                ' Error message for file open error.
                MessageBox.Show("Error opening the file.")
            End Try
        End If
    End Sub

    ' The SaveDocumnet procedure saves the current document.

    Sub SaveDocument()
        Dim outputFile As StreamWriter  ' Object variable

        Try
            ' Create the file.
            outputFile = File.CreateText(strFilename)

            ' Write the TextBox to the file.
            outputFile.Write(txtDocument.Text)

            ' Close the file.
            outputFile.Close()

            ' Update the isChanged variable.
            blnIsChanged = False
        Catch
            ' Error message for file creation error.
            MessageBox.Show("Error creating the file.")
        End Try
    End Sub


    Private Sub txtDocument_TextChanged(sender As Object, e As EventArgs) Handles txtDocument.TextChanged
        ' Indicate the text has changed.
        blnIsChanged = True
    End Sub


    Private Sub mnuFileNew_Click(sender As Object, e As EventArgs) Handles mnuFileNew.Click
        ' Has the current document changed?
        If blnIsChanged = True Then
            ' Confirm before clearing the document.
            If MessageBox.Show("The current document is not saved. " &
                               "Are you sure?", "Confirm", MessageBoxButtons.YesNo) =
                               Windows.Forms.DialogResult.Yes Then
                ClearDocumnet()
            End If
        Else
            ' Documnet has not changed, so clear it.
            ClearDocumnet()
        End If
    End Sub
End Class

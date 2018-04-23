<script type="text/javascript">
    function OnClick(s, e) {
        $.ajax({
            url: '@Url.Action("CallbackExport", "Home")',
            type: "POST",
            data: $("#form").serialize(),
            beforeSend: (function (data) {
                LoadingPanel.Show();
            })
        }).done(function (data) {
            LoadingPanel.Hide();
            $("#form").submit();
        });
    }
</script>
@using (Html.BeginForm("Export", "Home", FormMethod.Post, new With { .id = "form" }))
   
    @Html.Action("GridViewPartial")
    @Html.DevExpress().Button(Sub(settings)
                                  settings.Name = "btn"
                                  settings.Text = "Export"
                                  settings.ClientSideEvents.Click = "OnClick"
                              End Sub).GetHtml()
End Using 

@Html.DevExpress().LoadingPanel(Sub(settings)
                                    settings.Name = "LoadingPanel"
                                    settings.ContainerElementID = "form"
                                End Sub
).GetHtml()

<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128550586/15.1.11%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E5176)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [HomeController.cs](./CS/exportPanel/Controllers/HomeController.cs) (VB: [HomeController.vb](./VB/exportPanel/Controllers/HomeController.vb))
* [GridViewPartial.cshtml](./CS/exportPanel/Views/Home/GridViewPartial.cshtml)
* [Index.cshtml](./CS/exportPanel/Views/Home/Index.cshtml)
<!-- default file list end -->
# GridView - How to show LoadingPanel during export
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/e5176/)**
<!-- run online end -->


<p>When you export a grid with a large amount of data, you might want to show the LoadingPanel during exporting. To do so, handle the button's client-side Click event to:<br>1) Show the loading panel;<br>2) Perform an ajax request to the server to export the grid;<br>3) Hide the loading panel on success and submit the form to attach the exported document to the response;<br>4) Starting with version 15.1, it is necessary to call a special internal OnPost method in order to synchronize the internal grid's state to make it possible to collect and send it as custom request data (i.e., the callback state that stores end-user modifications such as sorting, grouping, filtering, selection, etc.). TODO: Use Ajax.BeginForm instead of jQuery.ajax</p>
<p> </p>


```js
function OnClick(s, e) {
	gridView.OnPost();
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
```


<p> </p>
<p><strong>See Also:</strong><br> <a href="https://www.devexpress.com/Support/Center/p/E2293">How to show ASPxLoadingPanel during export</a></p>

<br/>



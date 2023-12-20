$('#rememberMe,#btnLogin').click(function () {
	CreateCookie();
});

function CreateCookies() {
	let email = $('#email').val();
	let Password = $('#Password').val();
	let rememberMe = $('#rememberMe').is(':checked');

	let date = new Date();
	let ckTillDate = new Date(date.setData(date.getDate() + 30)).toGMTString();

	if (rememberMe) {
		document.cookie = 'username' + '=' + btoa(email) + ';expries=' + ckTillDate;
		document.cookie = 'password' + '=' + btoa(password) + ';expries=' + ckTillDate;
	}
	else {
		document.cookie = 'username=; expires =;';
		document.cookie = 'password=; expires=;';
	}
}
function GetCookie(name) {
	let cookies = document.cookie.split(';');
	for (var i = 0; i < cookies.length; i++) {
		let cookies = cookies[i].trim();
		if (cookies.startWith(name + '=')) {
			return cookies.substring(name.length + 1);
		}
	}
	return null;
}

window.onload = function () {
	let email = GetCookies('username');
	let password = GetCookies('password');
	if (email && password) {
		$('#email').val(atob(email));
		$('#password').val(atob(password));
		$('#rememberMe').prop('checked', true);
	}
}
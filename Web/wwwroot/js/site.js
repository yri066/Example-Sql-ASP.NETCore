const urlTrade = 'https://localhost:44355/api/Trade/GetLastTrade';

(function () {
	var elementExists = document.getElementById("Trade");

	if (elementExists) {
		getTrade(urlTrade);
	}

})();

function getTrade(requestURL) {
	var element = document.getElementById("Trade");

	var request = new XMLHttpRequest();
	request.open('GET', requestURL);
	request.responseType = 'json';
	request.withCredentials = true;

	request.send();

	request.onload = function () {

		if (request.status != 200) {
			element.innerHTML = `<span>Не удалось получить данные о сделке</span>`;
		}
		else {
			if (request.response != null) {
				var result = request.response;
				element.innerHTML = `<span>Cумма сделки: ${result.amount}</span><br>
				<span>Дата сделки: ${result.created}</span>`;
			}
			else {
				element.innerHTML = `<span>Последняя сделка не найдена</span>`;
			}
		}
	}

	request.onerror = function () {
		element.innerHTML = `<span>Не удалось получить данные о сделке</span>`;
	};
}
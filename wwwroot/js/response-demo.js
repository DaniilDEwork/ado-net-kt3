const demo = document.getElementById("responseDemo");
const action = demo.dataset.action;
const responseCode = document.getElementById("responseCode");
const responseType = document.getElementById("responseType");
const responseBody = document.getElementById("responseBody");
const repeatRequest = document.getElementById("repeatRequest");
const requestError = document.getElementById("requestError");

async function loadResponse() {
    repeatRequest.disabled = true;
    requestError.hidden = true;

    try {
        const response = await fetch(`/Test/${action}`, { cache: "no-store" });
        const text = await response.text();

        responseCode.textContent = `${response.status} ${response.statusText}`;
        responseCode.className = response.status >= 400 ? "error" : "";
        responseType.textContent = response.headers.get("Content-Type");
        responseBody.textContent = action === "Json"
            ? JSON.stringify(JSON.parse(text), null, 2)
            : text;

        if (action === "Html") {
            const htmlPreview = document.getElementById("htmlPreview");
            htmlPreview.srcdoc = text;
            htmlPreview.hidden = false;
            responseBody.hidden = true;
        }

        if (action === "File") {
            const disposition = response.headers.get("Content-Disposition") || "";
            const name = disposition.match(/filename="([^"]+)"/);
            document.getElementById("responseFile").textContent = name ? name[1] : "Имя не получено";
        }

        if (action === "Cookie") {
            const cookie = document.cookie.split("; ").find(value => value.startsWith("user="));
            document.getElementById("responseCookie").textContent = cookie || "Cookie не сохранена";
        }
    } catch {
        responseCode.textContent = "Ответ не получен";
        responseType.textContent = "Нет данных";
        responseBody.textContent = "";
        requestError.textContent = "Не удалось выполнить запрос, проверьте подключение к приложению";
        requestError.hidden = false;
    } finally {
        repeatRequest.disabled = false;
    }
}

repeatRequest.addEventListener("click", loadResponse);
loadResponse();
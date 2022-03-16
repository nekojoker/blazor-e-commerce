window.showInfoToast = (str) => {
    var message = document.getElementById("info-message");
    message.innerHTML = str;

    var element = document.getElementById("info-Toast");
    var toast = new bootstrap.Toast(element);
    toast.show();
};
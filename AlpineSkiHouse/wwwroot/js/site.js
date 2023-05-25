// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var Tool = {
    /**
     * 导出Excel（Blob）
     * @param {string} methd 请求类型
     * @param {string} url 请求地址（注意参数需要编码后传递）
     * @param {string} fileName 文件名
     */
    exportExcel: function (methd, url, fileName) {
        var oReq = new XMLHttpRequest()
        oReq.responseType = 'blob';
        oReq.open(methd, url)
        oReq.send();
        // 请求已完成，且响应已就绪
        oReq.onload = function (oEvent) {
            if (oReq.status >= 200 && oReq.status < 300) {
                var blob = oReq.response;
                var objectUrl = URL.createObjectURL(blob);
                if (window.navigator.msSaveBlob) {
                    window.navigator.msSaveBlob(blob, fileName)
                } else {
                    var a = document.createElement('a');
                    document.body.appendChild(a);
                    a.style = 'display:none'
                    a.href = objectUrl
                    a.download = fileName
                    a.click();
                    URL.revokeObjectURL(objectUrl);
                }
            }
        };
    },
    /**
     * 导出Excel（Form）
     * @param {string} methd 请求类型
     * @param {string} url 请求地址需要返回contentType为application/vnd.ms-excel类型的数据，自带文件名
     * @param {Array} childs [{Key:test,Value:test},{Key:test,Value:test}]
     */
    exportExcel: function (method, url, childs = []) {
        function createInput(dic) {
            var input = document.createElement('input')
            input.name = dic['Key']
            input.value = dic['Value']
            return input
        }
        var form = document.createElement('form')
        form.action = url
        form.enctype = 'multipart/form-data'
        form.method = method
        if (childs.length > 0) {
            for (var i = 0; i < childs.length; i++) {
                form.appendChild(createInput(childs[i]))
            }
        }
        document.body.appendChild(form)
        form.submit();
        form.remove();
    }
}
module.exports = function (callback, html) {
    var jsreport = require('jsreport-core')();

    jsreport.init().then(function () {
        return jsreport.render({
            template: {
                content: html,
                engine: 'jsrender',
                recipe: 'chrome-pdf',
                base: './wwwroot/'
            },
            data: {
                foo: "world"
            }
        }).then(function (resp) {
            callback(/* error */ null, resp.content.toJSON().data);
        });
    }).catch(function (e) {
        callback(/* error */ e, null);
    })
};
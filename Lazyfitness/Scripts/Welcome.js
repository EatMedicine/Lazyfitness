window.onload = function () {
    var vm = new Vue({
        el: '#app',
        data: {
            pageNum: 1,
            submitData: {
                password: "",
                webname: "",
                articlePartData: [
                    { pname: "" }
                ],
                forumPartData: [
                    { pname: "" }
                ],
                quesPartData: [
                    { pname: "" }
                ]
            }

        },
        methods: {
            AddPageNum: function () {
                tmp = this.pageNum;
                this.pageNum = -1;
                _this = this;
                setTimeout(function () {
                    _this.pageNum = tmp + 1;
                }, 500);
            },
            AddArticlePartNum: function () {
                tmp = { pname: "" };
                this.submitData.articlePartData.push(tmp);
            },
            AddForumPartNum: function () {
                tmp = { pname: "" };
                this.submitData.forumPartData.push(tmp);
            },
            AddQuesPartNum: function () {
                tmp = { pname: "" };
                this.submitData.quesPartData.push(tmp);
            }

        },
        computed: {
            submitJson: function () {
                return JSON.stringify(this.submitData);;
            }
        }

    });
};
(this["webpackJsonpwebjetmovies.ui"]=this["webpackJsonpwebjetmovies.ui"]||[]).push([[0],{115:function(e,t,n){"use strict";n.r(t);var i=n(5),r=n(0),c=n.n(r),a=n(12),s=n.n(a),o=(n(58),n(28)),u=(n(59),n(139)),j=n(138),l=n(140),b=n(137),p=n(136),m=n(142),v=n(24),d=n(141),f=n(44),h=n(39),x=Object(j.a)((function(e){return{inline:{display:"inline"},rightAlign:{fontSize:"24px",display:"inline",marginLeft:"400px"}}})),O=function(e){var t=x();return Object(i.jsxs)(d.a,{alignItems:"flex-start",children:[Object(i.jsx)(p.a,{children:Object(i.jsx)(m.a,{variant:"rounded",src:e.movie.poster,className:t.large,children:Object(i.jsx)(h.a,{icon:f.a})})}),Object(i.jsx)(b.a,{primary:e.movie.title,secondary:Object(i.jsxs)(c.a.Fragment,{children:[Object(i.jsx)(v.a,{component:"span",variant:"body2",className:t.inline,color:"textPrimary",children:e.movie.description}),Object(i.jsx)(v.a,{component:"span",variant:"body2",className:t.rightAlign,color:"textPrimary",children:e.movie.price})]})})]})},g=n(45),y=Object(j.a)((function(e){return{root:{width:"700px",backgroundColor:e.palette.background.paper}}}));function F(e){var t=y();return e.isFetching?Object(i.jsx)(g.Circle,{size:200}):Object(i.jsx)(u.a,{className:t.root,children:e.movies.map((function(e){return Object(i.jsxs)(i.Fragment,{children:[Object(i.jsx)(O,{movie:e}),Object(i.jsx)(l.a,{variant:"inset",component:"li"})]})}))})}var w=n(27),A=n.n(w),k=n(46),C=n(47),L=n.n(C),N={getMovieListAsync:function(){var e=Object(k.a)(A.a.mark((function e(){var t;return A.a.wrap((function(e){for(;;)switch(e.prev=e.next){case 0:return e.prev=0,e.next=3,L.a.get("/api/v1/movie/list");case 3:return t=e.sent,e.abrupt("return",t);case 7:e.prev=7,e.t0=e.catch(0),console.log("Failed to fetch items");case 10:return e.abrupt("return",[{}]);case 11:case"end":return e.stop()}}),e,null,[[0,7]])})));return function(){return e.apply(this,arguments)}}()};n(18).a.autoA11y=!0;var S=function(){var e=Object(r.useState)([{}]),t=Object(o.a)(e,2),n=t[0],c=t[1],a=Object(r.useState)(!0),s=Object(o.a)(a,2),u=s[0],j=s[1];return Object(r.useEffect)((function(){N.getMovieListAsync().then((function(e){200===e.status&&(c(e.data.movies),j(!1))}))}),[]),Object(i.jsx)("div",{className:"App",children:Object(i.jsx)(F,{movies:n,isFetching:u})})},P=function(e){e&&e instanceof Function&&n.e(3).then(n.bind(null,144)).then((function(t){var n=t.getCLS,i=t.getFID,r=t.getFCP,c=t.getLCP,a=t.getTTFB;n(e),i(e),r(e),c(e),a(e)}))};s.a.render(Object(i.jsx)(c.a.StrictMode,{children:Object(i.jsx)(S,{})}),document.getElementById("root")),P()},58:function(e,t,n){},59:function(e,t,n){}},[[115,1,2]]]);
//# sourceMappingURL=main.a55e0542.chunk.js.map
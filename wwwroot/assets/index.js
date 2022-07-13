const Q=function(){const e=document.createElement("link").relList;if(e&&e.supports&&e.supports("modulepreload"))return;for(const r of document.querySelectorAll('link[rel="modulepreload"]'))o(r);new MutationObserver(r=>{for(const c of r)if(c.type==="childList")for(const s of c.addedNodes)s.tagName==="LINK"&&s.rel==="modulepreload"&&o(s)}).observe(document,{childList:!0,subtree:!0});function n(r){const c={};return r.integrity&&(c.integrity=r.integrity),r.referrerpolicy&&(c.referrerPolicy=r.referrerpolicy),r.crossorigin==="use-credentials"?c.credentials="include":r.crossorigin==="anonymous"?c.credentials="omit":c.credentials="same-origin",c}function o(r){if(r.ep)return;r.ep=!0;const c=n(r);fetch(r.href,c)}};Q();function p(){}function W(t){return t&&typeof t=="object"&&typeof t.then=="function"}function K(t){return t()}function F(){return Object.create(null)}function E(t){t.forEach(K)}function X(t){return typeof t=="function"}function U(t,e){return t!=t?e==e:t!==e||t&&typeof t=="object"||typeof t=="function"}let S;function Y(t,e){return S||(S=document.createElement("a")),S.href=e,t===S.href}function Z(t){return Object.keys(t).length===0}function m(t,e){t.appendChild(e)}function A(t,e,n){t.insertBefore(e,n||null)}function C(t){t.parentNode.removeChild(t)}function y(t){return document.createElement(t)}function j(t){return document.createTextNode(t)}function $(){return j(" ")}function ee(t,e,n,o){return t.addEventListener(e,n,o),()=>t.removeEventListener(e,n,o)}function b(t,e,n){n==null?t.removeAttribute(e):t.getAttribute(e)!==n&&t.setAttribute(e,n)}function te(t){return Array.from(t.childNodes)}function ne(t,e){e=""+e,t.wholeText!==e&&(t.data=e)}let w;function g(t){w=t}function re(){if(!w)throw new Error("Function called outside component initialization");return w}const k=[],I=[],O=[],P=[],oe=Promise.resolve();let H=!1;function ce(){H||(H=!0,oe.then(z))}function q(t){O.push(t)}const T=new Set;let L=0;function z(){const t=w;do{for(;L<k.length;){const e=k[L];L++,g(e),le(e.$$)}for(g(null),k.length=0,L=0;I.length;)I.pop()();for(let e=0;e<O.length;e+=1){const n=O[e];T.has(n)||(T.add(n),n())}O.length=0}while(k.length);for(;P.length;)P.pop()();H=!1,T.clear(),g(t)}function le(t){if(t.fragment!==null){t.update(),E(t.before_update);const e=t.dirty;t.dirty=[-1],t.fragment&&t.fragment.p(t.ctx,e),t.after_update.forEach(q)}}const N=new Set;let x;function se(){x={r:0,c:[],p:x}}function ue(){x.r||E(x.c),x=x.p}function B(t,e){t&&t.i&&(N.delete(t),t.i(e))}function V(t,e,n,o){if(t&&t.o){if(N.has(t))return;N.add(t),x.c.push(()=>{N.delete(t),o&&(n&&t.d(1),o())}),t.o(e)}}function ie(t,e){const n=e.token={};function o(r,c,s,u){if(e.token!==n)return;e.resolved=u;let i=e.ctx;s!==void 0&&(i=i.slice(),i[s]=u);const l=r&&(e.current=r)(i);let h=!1;e.block&&(e.blocks?e.blocks.forEach((a,d)=>{d!==c&&a&&(se(),V(a,1,1,()=>{e.blocks[d]===a&&(e.blocks[d]=null)}),ue())}):e.block.d(1),l.c(),B(l,1),l.m(e.mount(),e.anchor),h=!0),e.block=l,e.blocks&&(e.blocks[c]=l),h&&z()}if(W(t)){const r=re();if(t.then(c=>{g(r),o(e.then,1,e.value,c),g(null)},c=>{if(g(r),o(e.catch,2,e.error,c),g(null),!e.hasCatch)throw c}),e.current!==e.pending)return o(e.pending,0),!0}else{if(e.current!==e.then)return o(e.then,1,e.value,t),!0;e.resolved=t}}function ae(t,e,n){const o=e.slice(),{resolved:r}=t;t.current===t.then&&(o[t.value]=r),t.current===t.catch&&(o[t.error]=r),t.block.p(o,n)}function fe(t){t&&t.c()}function J(t,e,n,o){const{fragment:r,on_mount:c,on_destroy:s,after_update:u}=t.$$;r&&r.m(e,n),o||q(()=>{const i=c.map(K).filter(X);s?s.push(...i):E(i),t.$$.on_mount=[]}),u.forEach(q)}function R(t,e){const n=t.$$;n.fragment!==null&&(E(n.on_destroy),n.fragment&&n.fragment.d(e),n.on_destroy=n.fragment=null,n.ctx=[])}function de(t,e){t.$$.dirty[0]===-1&&(k.push(t),ce(),t.$$.dirty.fill(0)),t.$$.dirty[e/31|0]|=1<<e%31}function D(t,e,n,o,r,c,s,u=[-1]){const i=w;g(t);const l=t.$$={fragment:null,ctx:null,props:c,update:p,not_equal:r,bound:F(),on_mount:[],on_destroy:[],on_disconnect:[],before_update:[],after_update:[],context:new Map(e.context||(i?i.$$.context:[])),callbacks:F(),dirty:u,skip_bound:!1,root:e.target||i.$$.root};s&&s(l.root);let h=!1;if(l.ctx=n?n(t,e.props||{},(a,d,...v)=>{const f=v.length?v[0]:d;return l.ctx&&r(l.ctx[a],l.ctx[a]=f)&&(!l.skip_bound&&l.bound[a]&&l.bound[a](f),h&&de(t,a)),d}):[],l.update(),h=!0,E(l.before_update),l.fragment=o?o(l.ctx):!1,e.target){if(e.hydrate){const a=te(e.target);l.fragment&&l.fragment.l(a),a.forEach(C)}else l.fragment&&l.fragment.c();e.intro&&B(t.$$.fragment),J(t,e.target,e.anchor,e.customElement),z()}g(i)}class G{$destroy(){R(this,1),this.$destroy=p}$on(e,n){const o=this.$$.callbacks[e]||(this.$$.callbacks[e]=[]);return o.push(n),()=>{const r=o.indexOf(n);r!==-1&&o.splice(r,1)}}$set(e){this.$$set&&!Z(e)&&(this.$$.skip_bound=!0,this.$$set(e),this.$$.skip_bound=!1)}}var me="/assets/svelte.png";function he(t){let e,n,o,r,c;return{c(){e=y("button"),n=j("Clicks: "),o=j(t[0]),b(e,"class","svelte-1c7643s")},m(s,u){A(s,e,u),m(e,n),m(e,o),r||(c=ee(e,"click",t[1]),r=!0)},p(s,[u]){u&1&&ne(o,s[0])},i:p,o:p,d(s){s&&C(e),r=!1,c()}}}function pe(t,e,n){let o=0;return[o,()=>{n(0,o+=1)}]}class _e extends G{constructor(e){super(),D(this,e,pe,he,U,{})}}function ge(t){return{c:p,m:p,p,d:p}}function be(t){let e=JSON.stringify(t[1])+"",n;return{c(){n=j(e)},m(o,r){A(o,n,r)},p,d(o){o&&C(n)}}}function ve(t){let e;return{c(){e=y("progress")},m(n,o){A(n,e,o)},p,d(n){n&&C(e)}}}function ye(t){let e,n,o,r,c,s,u,i,l,h,a,d,v;u=new _e({});let f={ctx:t,current:null,token:null,hasCatch:!1,pending:ve,then:be,catch:ge,value:1};return ie(t[0],f),{c(){e=y("main"),n=y("img"),r=$(),c=y("h1"),c.textContent="Hello .NET6 world!",s=$(),fe(u.$$.fragment),i=$(),f.block.c(),l=$(),h=y("p"),h.innerHTML=`Visit <a href="https://svelte.dev">svelte.dev</a> to learn how to build Svelte
    apps.`,a=$(),d=y("p"),d.innerHTML=`Check out <a href="https://github.com/sveltejs/kit#readme">SvelteKit</a> for
    the officially supported framework, also powered by Vite!`,Y(n.src,o=me)||b(n,"src",o),b(n,"alt","Svelte Logo"),b(n,"class","svelte-1fm71xa"),b(c,"class","svelte-1fm71xa"),b(h,"class","svelte-1fm71xa"),b(d,"class","svelte-1fm71xa"),b(e,"class","svelte-1fm71xa")},m(_,M){A(_,e,M),m(e,n),m(e,r),m(e,c),m(e,s),J(u,e,null),m(e,i),f.block.m(e,f.anchor=null),f.mount=()=>e,f.anchor=l,m(e,l),m(e,h),m(e,a),m(e,d),v=!0},p(_,[M]){t=_,ae(f,t,M)},i(_){v||(B(u.$$.fragment,_),v=!0)},o(_){V(u.$$.fragment,_),v=!1},d(_){_&&C(e),R(u),f.block.d(),f.token=null,f=null}}}function xe(t){return[fetch("/api/counter").then(n=>n.json()).then(n=>(console.log(n),n)).catch(n=>(console.log(n),n))]}class $e extends G{constructor(e){super(),D(this,e,xe,ye,U,{})}}new $e({target:document.getElementById("app")});

import { check } from 'k6';
import http from 'k6/http';

export let options = {
    discardResponseBodies: true,
    scenarios: {
        weatherForecastAsynctacts: {
            executor: 'constant-arrival-rate',
            rate: 1000,
            timeUnit: '1s', // 1000 iterations per second, i.e. 1000 RPS
            duration: '30s',
            preAllocatedVUs: 10, // the size of the VU (i.e. worker) pool for this scenario
            tags: { test_type: 'weatherForecastAsync' }, // different extra metric tags for this scenario
            exec: 'weatherForecastAsync', // this scenario is executing different code than the one above!
        },
        // weatherForecast: {
        //     executor: 'constant-arrival-rate',
        //     rate: 1000,
        //     timeUnit: '1s', // 1000 iterations per second, i.e. 1000 RPS
        //     duration: '30s',
        //     preAllocatedVUs: 10, // the size of the VU (i.e. worker) pool for this scenario
        //     tags: { test_type: 'weatherForecast' }, // different extra metric tags for this scenario
        //     exec: 'weatherForecast', // this scenario is executing different code than the one above!
        // },
    },
};

export function weatherForecastAsync() {
    let res = http.get('http://localhost:5000/WeatherForecastAsync');
    check(res, {
        'is status 200': (r) => r.status === 200
    });
}

export function weatherForecast() {
    let res = http.get('http://localhost:5000/WeatherForecast');
    check(res, {
        'is status 200': (r) => r.status === 200
    });
}
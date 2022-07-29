# Start RMQ
```
docker run --rm -d --name rmq -p 15672:15672 -p 5672:5672 masstransit/rabbitmq
```
And trail the logs with
```
docker logs rmq --follow
```

# Stop RMQ
```
docker stop rmq
```


# Log into RMQ
You can access the dashboard [here](http://localhost:15672/) with `guest`/`guest`
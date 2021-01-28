# Hard to say

## Our Requirements

Minimal downtime — our goal was a hard limit of 4 hours of cumulative downtime, including unintended outages resulting from mistakes during the upgrade.
Build out a new cluster of databases on new instances to replace our current fleet of aging servers.
Go up to i3.16xlarge instances so we have headroom to grow.

There are three methods of performing Postgres upgrades that we were aware of: the classic backup and restore process, pg_upgrade, and logical replication.

We immediately gave up on the backup and restore method as it would take far too long on our 5.7TB dataset. pg_upgrade, while fast, is an in-place upgrade and did not satisfy conditions 2 and 3. So, we moved forward with logical replication.
The Process

There is a lot of existing literature about nitty gritty of installing and using pglogical, so rather than repeat the existing material I’ll just link a few articles that I found immensely helpful:

https://www.depesz.com/2016/11/08/major-version-upgrading-with-minimal-downtime/

https://info.crunchydata.com/blog/upgrading-postgresql-from-9.4-to-10.3-with-pglogical

http://thedumbtechguy.blogspot.com/2017/04/demystifying-pglogical-tutorial.html

We created one Postgres 12 server that would become our new primary, and used pglogical to synchronize all our data. Once it caught up and was replicating incoming changes, we began adding streaming replicas behind it. As we provisioned each new streaming replica, we would add it to HAProxy while removing one of the old 9.6 replicas. We did this until we had retired all of our Postgres 9.6 servers except the primary, which put us in the following state:
